"use strict";

var gl
var gl2d
var input

const max_lights = 10
const gpu_positions_attrib_location = 0
const gpu_normals_attrib_location = 1
const gpu_tex_coord_attrib_location = 2

var mvp_matrix
var camera
var point_lights
var ambient_light
var directional_light
var light_fx

var cube
var simpleCube
var pyramid
var texture_col1
var texture_col2

var score = 0
var time = 0
var player
var collectibles

function Float32Concat(first, second) {
    var result = new Float32Array(first.length + second.length)
    result.set(first)
    result.set(second, first.length)
    return result
}

function Camera() {
	this.ubo = gl.createBuffer()
	this.position = new Float32Array([0, 0, 0])
	
	gl.bindBuffer(gl.UNIFORM_BUFFER, this.ubo)
	gl.bufferData(gl.UNIFORM_BUFFER, this.position, gl.DYNAMIC_DRAW)
	
	this.setPosition = function(x = this.position[0], y = this.position[1], z = this.position[2]) {
		this.position.set([x, y, z])
	}
	
	this.updateUBO = function() {
		gl.bindBuffer(gl.UNIFORM_BUFFER, this.ubo)
		gl.bufferSubData(gl.UNIFORM_BUFFER, 0, this.position)
		
		mat4.lookAt(mvp_matrix.view, this.position, new Float32Array(player.position), new Float32Array([0., 1., 0.]))
	}
}

function MVPMatrix() {
	this.ubo = gl.createBuffer()
	this.base = mat4.create()
	this.view = mat4.create()
    mat4.lookAt(this.view, camera.position, new Float32Array(player.position), new Float32Array([0., 1., 0.]))
	this.projection = mat4.create()
	mat4.perspective(this.projection, Math.PI/4., gl.drawingBufferWidth/gl.drawingBufferHeight, 0.01, 100)
	this.model = mat4.create()
	
    mat4.multiply(this.base, this.projection, this.view)
	
	gl.bindBuffer(gl.UNIFORM_BUFFER, this.ubo)
	gl.bufferData(gl.UNIFORM_BUFFER, Float32Concat(this.base, this.model), gl.DYNAMIC_DRAW)
	
	this.setModel = function(model) {
		this.model = model
		mat4.multiply(this.base, this.projection, this.view)
		mat4.multiply(this.base, this.base, this.model)
	}
	
	this.updateUBO = function() {
		gl.bindBuffer(gl.UNIFORM_BUFFER, this.ubo)
		gl.bufferSubData(gl.UNIFORM_BUFFER, 0, Float32Concat(this.base, this.model))
	}
}

function PointLightManager() {
	this.ubo = gl.createBuffer()
	this.material = gl.createBuffer()
	this.defaultMaterial = [1, 1, 1, 1, 256]
	this.lights = []
	
    gl.bindBuffer(gl.UNIFORM_BUFFER, this.ubo)
    gl.bufferData(gl.UNIFORM_BUFFER, new Float32Array(max_lights*8 + 4), gl.DYNAMIC_DRAW)
    gl.bindBuffer(gl.UNIFORM_BUFFER, this.material)
    gl.bufferData(gl.UNIFORM_BUFFER, new Float32Array(this.defaultMaterial), gl.DYNAMIC_DRAW)
	
	this.updateUBO = function() {
		var data = new Array(max_lights*8 + 4).fill(0)
		data.splice(0, 1, this.lights.length)
		this.lights.forEach(function(light, i) {
			data.splice.apply(data, [i*8 + 4, 8].concat(light.position).concat([light.radius]).concat(light.color).concat([0]))
		})
		
		gl.bindBuffer(gl.UNIFORM_BUFFER, this.ubo)
		gl.bufferData(gl.UNIFORM_BUFFER, new Float32Array(data), gl.DYNAMIC_DRAW)
	}
	
	this.overrideMaterial = function(material) {
		gl.bindBuffer(gl.UNIFORM_BUFFER, this.material)
		gl.bufferData(gl.UNIFORM_BUFFER, new Float32Array(material), gl.DYNAMIC_DRAW)
	}
	
	this.revertMaterial = function() {
		gl.bindBuffer(gl.UNIFORM_BUFFER, this.material)
		gl.bufferData(gl.UNIFORM_BUFFER, new Float32Array(this.defaultMaterial), gl.DYNAMIC_DRAW)
	}
	
	this.addLight = function(light) {
		this.lights.push(light)
	}
	
	this.get = function(index) {
		return this.lights[index]
	}
}

function PointLight() {
	this.position = [0, 0, 0]
	this.color = [1, 1, 1]
	this.radius = 16
	
	this.setPosition = function(x = this.position[0], y = this.position[1], z = this.position[2]) {
		this.position = [x, y, z]
	}
	
	this.translate = function(dx = 0, dy = 0, dz = 0) {
		this.position = this.position.map(function(value, index) {return value + [dx, dy, dz][index]})
	}
	
	this.setColor = function(r = this.color[0], g = this.color[1], b = this.color[2]) {
		this.color = [r, g, b]
	}
}

function Texture(src) {
	var _this = this
    this.data = gl.createTexture()
    gl.bindTexture(gl.TEXTURE_2D, this.data)
    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, 1, 1, 0, gl.RGBA, gl.UNSIGNED_BYTE, new Uint8Array(255, 255, 255, 255))
	
    var image = new Image()
    image.src = src
    image.addEventListener('load', function() {
        gl.bindTexture(gl.TEXTURE_2D, _this.data)
        gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image)
        gl.generateMipmap(gl.TEXTURE_2D)
    })
}

function Model(indices, vertices) {
	if (vertices[3] == 0) countNormals(vertices, indices)
	
    this.vbo = gl.createBuffer()
    gl.bindBuffer(gl.ARRAY_BUFFER, this.vbo)
    gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW)
    gl.bindBuffer(gl.ARRAY_BUFFER, null)
	
    this.index_buffer = gl.createBuffer()
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, this.index_buffer)
    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, indices, gl.STATIC_DRAW)
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, null)

    this.vao = gl.createVertexArray()
    gl.bindVertexArray(this.vao)
    gl.bindBuffer(gl.ARRAY_BUFFER, this.vbo)
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, this.index_buffer)
    gl.enableVertexAttribArray(gpu_positions_attrib_location)
    gl.vertexAttribPointer(gpu_positions_attrib_location, 3, gl.FLOAT, gl.FALSE, 8*4, 0)
    gl.enableVertexAttribArray(gpu_normals_attrib_location)
    gl.vertexAttribPointer(gpu_normals_attrib_location, 3, gl.FLOAT, gl.FALSE, 8*4, 3*4)
    gl.enableVertexAttribArray(gpu_tex_coord_attrib_location)
    gl.vertexAttribPointer(gpu_tex_coord_attrib_location, 2, gl.FLOAT, gl.FALSE, 8*4, 6*4)
    gl.bindVertexArray(null)
    gl.bindBuffer(gl.ARRAY_BUFFER, null)
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, null)
	
	this.position = new Float32Array([0, 0, 0])
	this.rotation = new Float32Array([0, 0, 0])
	
	this.draw = function() {
		var model_matrix = mat4.create()
		mat4.translate(model_matrix, model_matrix, this.position)
		mat4.rotateX(model_matrix, model_matrix, this.rotation[0])
		mat4.rotateY(model_matrix, model_matrix, this.rotation[1])
		mat4.rotateZ(model_matrix, model_matrix, this.rotation[2])
		// mat3.invert(model_matrix, model_matrix)
		// mat3.transpose(model_matrix, model_matrix)
		mvp_matrix.setModel(model_matrix)
		mvp_matrix.updateUBO()
		
		gl.bindVertexArray(this.vao)
		gl.drawElements(gl.TRIANGLES, indices.length, gl.UNSIGNED_SHORT, 0)
	}
	
	this.setPosition = function(position) {
		this.position.set(position)
	}
	
	this.setRotation = function(rotation) {
		this.rotation.set(rotation)
	}
}

function GameObject(model, texture, properties) {
	this.model = model
	this.texture = texture
	this.properties = properties
	this.position = [0, 0, 0]
	this.rotation = [0, 0, 0]
	
	this.setPosition = function(x = this.position[0], y = this.position[1], z = this.position[2]) {
		this.position = [x, y, z]
	}
	
	this.setRotation = function(rotX = this.rotation[0], rotY = this.rotation[1], rotZ = this.rotation[2]) {
		this.rotation = [rotX % (2*Math.PI), rotY % (2*Math.PI), rotZ % (2*Math.PI)]
	}
	
	this.translate = function(dx = 0, dy = 0, dz = 0) {
		this.position = this.position.map(function(value, index) {return value + [dx, dy, dz][index]})
	}
	
	this.rotate = function(dx = 0, dy = 0, dz = 0) {
		this.rotation = this.rotation.map(function(value, index) {return value + [dx, dy, dz][index]})
	}
	
	this.draw = function() {
		gl.activeTexture(gl.TEXTURE0)
		if (this.isProcedural) {
			gl.bindBuffer(gl.UNIFORM_BUFFER, Model.prototype.procUbo)
			gl.bufferSubData(gl.UNIFORM_BUFFER, 0, new Float32Array([1]))
		} else {
			gl.bindBuffer(gl.UNIFORM_BUFFER, Model.prototype.procUbo)
			gl.bufferSubData(gl.UNIFORM_BUFFER, 0, new Float32Array([0]))
			gl.bindTexture(gl.TEXTURE_2D, this.texture.data)
		}
	
		this.model.setPosition(this.position)
		this.model.setRotation(this.rotation)
		this.model.draw()
	}
	
	this.makeTextureProcedural = function() {
		this.isProcedural = true
	}
}

function init() {
	input = {pressed: new Set(), press: function(id) {return this.pressed.has(id)}}
	document.addEventListener('keydown', function(event) {input.pressed.add(event.key)})
	document.addEventListener('keyup', function(event) {input.pressed.delete(event.key)})
	
    try {
        let canvas = document.querySelector("#glcanvas")
        gl = canvas.getContext("webgl2")
    }
    catch(e) {
    }

    if (!gl)
    {
        alert("Unable to initialize WebGL.")
        return
    }
	
	var textCanvas = document.getElementById("text")
	gl2d = textCanvas.getContext("2d")
	gl2d.fillStyle = 'white'
	gl2d.font = '20pt Calibri'
	
    gl.viewport(0, 0, gl.drawingBufferWidth, gl.drawingBufferHeight)

	gl.clearColor(0.03, 0.03, 0.03, 1)
	gl.enable(gl.DEPTH_TEST)
	gl.enable(gl.CULL_FACE)
	gl.cullFace(gl.BACK)

    var program = createProgram(gl, createShader(gl, gl.VERTEX_SHADER, vertex_shader), createShader(gl, gl.FRAGMENT_SHADER, fragment_shader))

    var matrices_ubi = gl.getUniformBlockIndex(program, "Matrices")
    var cam_info_ubi = gl.getUniformBlockIndex(program, "CamInfo")
    var material_ubi = gl.getUniformBlockIndex(program, "Material")
    var point_light_ubi = gl.getUniformBlockIndex(program, "PointLights")
    var ambient_light_ubi = gl.getUniformBlockIndex(program, "AmbientLight")
    var directional_light_ubi = gl.getUniformBlockIndex(program, "DirectionalLight")
    var procedural_texture_ubi = gl.getUniformBlockIndex(program, "ProceduralTexture")

    let matrices_ubb = 0
    gl.uniformBlockBinding(program, matrices_ubi, matrices_ubb)
    let cam_info_ubb = 1
    gl.uniformBlockBinding(program, cam_info_ubi, cam_info_ubb)
    let material_ubb = 2
    gl.uniformBlockBinding(program, material_ubi, material_ubb)
    let point_light_ubb = 3
    gl.uniformBlockBinding(program, point_light_ubi, point_light_ubb)
    let ambient_light_ubb = 4
    gl.uniformBlockBinding(program, ambient_light_ubi, ambient_light_ubb)
    let directional_light_ubb = 5
    gl.uniformBlockBinding(program, directional_light_ubi, directional_light_ubb)
    let procedural_texture_ubb = 6
    gl.uniformBlockBinding(program, procedural_texture_ubi, procedural_texture_ubb)

    var linear_sampler = gl.createSampler()
    gl.samplerParameteri(linear_sampler, gl.TEXTURE_WRAP_S, gl.REPEAT)
    gl.samplerParameteri(linear_sampler, gl.TEXTURE_WRAP_T, gl.REPEAT)
    gl.samplerParameteri(linear_sampler, gl.TEXTURE_WRAP_R, gl.REPEAT)
    gl.samplerParameteri(linear_sampler, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_LINEAR)
    gl.samplerParameteri(linear_sampler, gl.TEXTURE_MAG_FILTER, gl.LINEAR)
    
	var texture_player = new Texture("images/Character_diffuse.png")
	texture_col1 = new Texture("images/Collectible_diffuse.png")
	texture_col2 = new Texture("images/Collectible2_diffuse.png")
	
	Model.prototype.procUbo = gl.createBuffer()
	gl.bindBuffer(gl.UNIFORM_BUFFER, Model.prototype.procUbo)
	gl.bufferData(gl.UNIFORM_BUFFER, new Float32Array([0]), gl.DYNAMIC_DRAW)
	
    simpleCube = new Model(new Uint16Array([0, 3, 1,
											1, 3, 2,
											4, 6, 5,
											4, 7, 6,
											0, 4, 5,
											0, 5, 1,
											6, 1, 5,
											6, 2, 1,
											7, 3, 6,
											3, 2, 6,
											4, 0, 3,
											7, 4, 3]),
							new Float32Array([-0.25, -0.25, -0.25,		-Math.sqrt(3),-Math.sqrt(3),-Math.sqrt(3), 		0,1,
											0.25, -0.25, -0.25,			Math.sqrt(3),-Math.sqrt(3),-Math.sqrt(3),		1,1,
											0.25, -0.25, 0.25,			Math.sqrt(3),-Math.sqrt(3),Math.sqrt(3),		1,0,
											-0.25, -0.25, 0.25,			-Math.sqrt(3),-Math.sqrt(3),Math.sqrt(3),		0,0,
											-0.25, 0.25, -0.25,			-Math.sqrt(3),Math.sqrt(3),-Math.sqrt(3), 		0,0,
											0.25, 0.25, -0.25,			Math.sqrt(3),Math.sqrt(3),-Math.sqrt(3),		1,0,
											0.25, 0.25, 0.25,			Math.sqrt(3),Math.sqrt(3),Math.sqrt(3),			1,1,
											-0.25, 0.25, 0.25,			-Math.sqrt(3),Math.sqrt(3),Math.sqrt(3),		0,1])
	)
	
    cube = new Model(new Uint16Array([16, 17, 18,
									16, 18, 19,
									20, 22, 21,
									20, 23, 22,
									0, 4, 5,
									0, 5, 1,
									11, 8, 10,
									11, 9, 8,
									7, 3, 6,
									3, 2, 6,
									14, 13, 15,
									14, 12, 13]),
					new Float32Array([-0.5,  -0.5, -0.5,	0,0,0, 		0.25,0.75,
									0.5, -0.5, -0.5,		0,0,0,		0,0.75,
									0.5, -0.5, 0.5,			0,0,0,		0.75,0.75,
									-0.5, -0.5, 0.5,		0,0,0,		0.5,0.75,
									-0.5, 0.5, -0.5,		0,0,0, 		0.25,0.5,
									0.5, 0.5, -0.5,			0,0,0,		0,0.5,
									0.5, 0.5, 0.5,			0,0,0,		0.75,0.5,
									-0.5, 0.5, 0.5,			0,0,0,		0.5,0.5,
									0.5, -0.5, -0.5,		0,0,0,		0.5,0.5,
									0.5, -0.5, 0.5,			0,0,0,		0.25,0.5,
									0.5, 0.5, -0.5,			0,0,0,		0.5,0.25,
									0.5, 0.5, 0.5,			0,0,0,		0.25,0.25,
									-0.5, -0.5, -0.5,		0,0,0, 		0.25,1,
									-0.5, -0.5, 0.5,		0,0,0,		0.5,1,
									-0.5, 0.5, -0.5,		0,0,0, 		0.25,0.75,
									-0.5, 0.5, 0.5,			0,0,0,		0.5,0.75,
									-0.5, -0.5, -0.5,		0,0,0, 		0.75,0.5,
									0.5, -0.5, -0.5,		0,0,0,		1,0.5,
									0.5, -0.5, 0.5,			0,0,0,		1,0.75,
									-0.5, -0.5, 0.5,		0,0,0,		0.75,0.75,
									-0.5, 0.5, -0.5,		0,0,0, 		0.25,0.5,
									0.5, 0.5, -0.5,			0,0,0,		0.5,0.5,
									0.5, 0.5, 0.5,			0,0,0,		0.5,0.75,
									-0.5, 0.5, 0.5,			0,0,0,		0.25,0.75])
	)
	
	pyramid = new Model(new Uint16Array([1, 8, 0,
									3, 13, 2,
									5, 14, 4,
									7, 15, 6,
									11, 10, 9,
									12, 11, 9]),
							new Float32Array([-0.5, -0.5, 0.5,		0,0,0, 		0,0,
												0.5, -0.5, 0.5,		0,0,0,		1,0,
												0.5, -0.5, 0.5,		0,0,0,		0,0,
												0.5, -0.5, -0.5,	0,0,0,		1,0,
												0.5, -0.5, -0.5,	0,0,0,		0,0,
												-0.5, -0.5, -0.5,	0,0,0,		1,0,
												-0.5, -0.5, -0.5,	0,0,0,		0,0,
												-0.5, -0.5, 0.5,	0,0,0,		1,0,
												0, 0.5, 0,			0,0,0,		0.5,1,
												-0.5, -0.5, 0.5,	0,0,0,		0,0,
												0.5, -0.5, 0.5,		0,0,0,		1,0,
												0.5, -0.5, -0.5,	0,0,0,		1,1,
												-0.5, -0.5, -0.5,	0,0,0,		0,1,
												0, 0.5, 0,			0,0,0,		0.5,1,
												0, 0.5, 0,			0,0,0,		0.5,1,
												0, 0.5, 0,			0,0,0,		0.5,1])
	)
	
	var al_ubo = gl.createBuffer()
	gl.bindBuffer(gl.UNIFORM_BUFFER, al_ubo)
	gl.bufferData(gl.UNIFORM_BUFFER, new Float32Array([0, 0, 0]), gl.DYNAMIC_DRAW)
	
	ambient_light = {ubo: al_ubo,
		color: [0, 0, 0],
		setColor: function(r, g, b) {
			this.color = [r, g, b]
			gl.bindBuffer(gl.UNIFORM_BUFFER, this.ubo)
			gl.bufferSubData(gl.UNIFORM_BUFFER, 0, new Float32Array([r, g, b]))
	}}
	
	var dl_ubo = gl.createBuffer()
	const dl_color = [0.05, 0.05, 0.05]
	gl.bindBuffer(gl.UNIFORM_BUFFER, dl_ubo)
	gl.bufferData(gl.UNIFORM_BUFFER, new Float32Array(dl_color.concat([0, -0.5 + Math.random(), 0, -0.5 + Math.random()])), gl.DYNAMIC_DRAW)
	directional_light = {ubo: dl_ubo,
		setDirection: function(x, y, z) {
			console.log([x, y, z])
			gl.bindBuffer(gl.UNIFORM_BUFFER, this.ubo)
			gl.bufferSubData(gl.UNIFORM_BUFFER, 0, new Float32Array(dl_color.concat([0, x, y, z])))
	}}
	
	player = new GameObject(cube, texture_player)
	camera = new Camera()
	camera.angle = 0
	mvp_matrix = new MVPMatrix()
	point_lights = new PointLightManager()
	point_lights.addLight(new PointLight())
	point_lights.addLight(new PointLight())
	point_lights.addLight(new PointLight())
	
	light_fx = []
	light_fx.push(new GameObject(simpleCube))
	light_fx.push(new GameObject(simpleCube))
	light_fx.push(new GameObject(simpleCube))
	light_fx.forEach(function(fx) {fx.makeTextureProcedural()})
	light_fx[0].parent = point_lights.get(0)
	light_fx[1].parent = point_lights.get(1)
	light_fx[2].parent = point_lights.get(2)
	
	player.setRotation(0, Math.PI)
	point_lights.get(0).radius = 8
	point_lights.get(0).setColor(1, 0, 0)
	point_lights.get(1).radius = 8
	point_lights.get(1).setColor(0, 0, 1)
	point_lights.get(2).radius = 8
	point_lights.get(2).setColor(0, 1, 0)
	point_lights.get(2).direction = Math.random() * Math.PI * 2
	
	collectibles = new Set()
	for (var i = 0; i < 8; i++) collectibles.add(newCollectible())
	
    gl.bindSampler(0, linear_sampler)
	
    gl.useProgram(program)
    gl.bindBufferBase(gl.UNIFORM_BUFFER, matrices_ubb, mvp_matrix.ubo)
    gl.bindBufferBase(gl.UNIFORM_BUFFER, cam_info_ubb, camera.ubo)
    gl.bindBufferBase(gl.UNIFORM_BUFFER, point_light_ubb, point_lights.ubo)
    gl.bindBufferBase(gl.UNIFORM_BUFFER, material_ubb, point_lights.material)
    gl.bindBufferBase(gl.UNIFORM_BUFFER, ambient_light_ubb, al_ubo)
    gl.bindBufferBase(gl.UNIFORM_BUFFER, directional_light_ubb, dl_ubo)
    gl.bindBufferBase(gl.UNIFORM_BUFFER, procedural_texture_ubb, Model.prototype.procUbo)
}

function update() {
	time++
	
	if (input.press("w"))
		player.translate(Math.sin(player.rotation[1]) * 0.1, 0, Math.cos(player.rotation[1]) * 0.1)
	if (input.press("s"))
		player.translate(Math.sin(player.rotation[1]) * -0.05, 0, Math.cos(player.rotation[1]) * -0.05)
	
	if (input.press("d"))
		player.rotate(undefined, -0.1)
	if (input.press("a"))
		player.rotate(undefined, 0.1)
	
	if (input.press("ArrowRight"))
		camera.angle -= 0.05
	if (input.press("ArrowLeft"))
		camera.angle += 0.05
	
	var old = Math.abs(score)
	var collected = new Set()
	collectibles.forEach(function(obj) {
		if (Math.abs(player.position[0] - obj.position[0]) < 1 &&
			Math.abs(player.position[1] - obj.position[1]) < 1 &&
			Math.abs(player.position[2] - obj.position[2]) < 1) {
			collected.add(obj)
			
			if (obj.properties[0]) score++
			else score--;
		} else {
			if (--obj.properties[1] == 0) {
				collected.add(obj)
			}
		}
	})
	collected.forEach(function(obj) {collectibles.delete(obj)})
	
	if (collectibles.size < 12 && Math.random()*10000 < 500 - collectibles.size*10 - time/10) collectibles.add(newCollectible())
	
	if (ambient_light.fade > 0) {
		ambient_light.fade -= 0.01
		ambient_light.setColor(ambient_light.fade, ambient_light.fade, ambient_light.fade)
	} else if (ambient_light.fade < 0) {
		ambient_light.fade = 0
		ambient_light.setColor(ambient_light.fade, ambient_light.fade, ambient_light.fade)
	}

	if (Math.abs(score) < old) {
		ambient_light.fade = 0.6
	} else if (Math.abs(score) > old) {
		directional_light.setDirection(-0.5 + Math.random(), 0, -0.5 + Math.random())
	}

	const light_dist = 3
	point_lights.get(0).setPosition(player.position[0] + Math.sin(Date.now() / 250)*light_dist, 2, player.position[2] + Math.cos(Date.now() / 250)*light_dist)
	point_lights.get(1).setPosition(player.position[0] - Math.sin(Date.now() / 250)*light_dist, 2, player.position[2] - Math.cos(Date.now() / 250)*light_dist)
	point_lights.get(2).setColor(Math.abs(Math.sin(Date.now() / 2000)), undefined, Math.abs(Math.cos(Date.now() / 2000)))
	
	var runner = point_lights.get(2)
	const runner_speed = 0.1
	if (Date.now() % 1000 < 100 && Math.random() < 0.6)
		runner.direction += -Math.PI/4 + Math.random() * Math.PI/2
	if (runner.position[0] > 10) runner.direction = Math.PI * 3/2
	if (runner.position[0] < -10) runner.direction = Math.PI * 1/2
	if (runner.position[2] > 10) runner.direction = Math.PI
	if (runner.position[2] < -10) runner.direction = 0
	point_lights.get(2).translate(Math.sin(point_lights.get(2).direction) * runner_speed, 0, Math.cos(point_lights.get(2).direction) * runner_speed)
	
	light_fx.forEach(function(fx) {fx.setPosition.apply(fx, fx.parent.position)})
	camera.setPosition(player.position[0] + Math.sin(camera.angle) * 16, 12, player.position[2] + Math.cos(camera.angle) * 16)
}

function draw() {
	update()
	
	gl2d.clearRect(0, 0, gl2d.canvas.width, gl2d.canvas.height)
	gl2d.fillText("Wynik: " + Math.abs(score), 16, 32)
	if (time < 5000)  {gl2d.fillText("Czas: " + Math.ceil((5000 - time)/60), 16, 56)
	gl2d.fillText("Liczba piramid: " + collectibles.size, 16, 80)}
	
    gl.clear(gl.COLOR_BUFFER_BIT)
	point_lights.updateUBO()
	camera.updateUBO()
	
	var old_color = ambient_light.color
	ambient_light.setColor(1, 1, 1)
	light_fx.forEach(function(fx) {
		point_lights.overrideMaterial(fx.parent.color.concat([1, 256]))
		fx.draw()
	})
	ambient_light.setColor.apply(ambient_light, old_color)
	point_lights.revertMaterial()
	
	player.draw()
	collectibles.forEach(function(obj) {
		if (obj.properties[1] > 60 || time%10 < 5) obj.draw()}
	)

    window.requestAnimationFrame(draw)
}

function main() {
    init()
    draw()
}

function createShader(gl, type, source) {
    var shader = gl.createShader(type)
    gl.shaderSource(shader, source)
    gl.compileShader(shader)
    var success = gl.getShaderParameter(shader, gl.COMPILE_STATUS)
    if(success)
    {
        return shader
    }

    console.log(gl.getShaderInfoLog(shader))
    gl.deleteShader(shader)
}

function createProgram(gl, vertex_shader, fragment_shader) {
    var program = gl.createProgram()
    gl.attachShader(program, vertex_shader)
    gl.attachShader(program, fragment_shader)
    gl.linkProgram(program)
    var success = gl.getProgramParameter(program, gl.LINK_STATUS)
    if(success)
    {
        return program
    }

    console.log(gl.getProgramInfoLog(program))
    gl.deleteProgram(program)
}

function sub(vec1, vec2) {
	return vec1.map(function(val, i) {return val - vec2[i]})
}

function countNormals(vertices, indices) {
	for (var i = 0; i < indices.length/3; i++) {
		var i1 = indices[i*3]*8
		var i2 = indices[i*3 + 1]*8
		var i3 = indices[i*3 + 2]*8
		
		var n1 = sub(vertices.slice(i1, i1+3), vertices.slice(i2, i2+3))
		var n2 = sub(vertices.slice(i1, i1+3), vertices.slice(i3, i3+3))
		
		var normal = [n1[1]*n2[2] - n2[1]*n1[2], n1[2]*n2[0] - n2[2]*n1[0], n1[0]*n2[1] - n2[0]*n1[1]]
		var len = Math.sqrt(normal[0]*normal[0] + normal[1]*normal[1] + normal[2]*normal[2])
		
		for (var j = 0; j < 3; j++) {
			vertices[i1 + 3 + j] = normal[j] / len
			vertices[i2 + 3 + j] = normal[j] / len
			vertices[i3 + 3 + j] = normal[j] / len
		}
	}
}

function newCollectible() {
	var red = (Math.random() <= 0.5)
	var obj = new GameObject(pyramid, red ? texture_col1 : texture_col2, [red, 180 + Math.floor(Math.random()*120)])
	obj.setPosition(-10 + Math.random()*20, 0, -10 + Math.random()*20)
	while (Math.abs(player.position[0] - obj.position[0]) < 5 &&
		Math.abs(player.position[1] - obj.position[1]) < 5 &&
		Math.abs(player.position[2] - obj.position[2]) < 5)
		obj.setPosition(-10 + Math.random()*20, 0, -10 + Math.random()*20)
	
	obj.setRotation(0, Math.random() * Math.PI*2, 0)
	
	return obj
}

var vertex_shader = "#version 300 es\n\
    layout(location = 0) in vec3 vertex_position;\n\
    layout(location = 1) in vec3 vertex_normal;\n\
    layout(location = 2) in vec2 vertex_tex_coord;\n\
    out vec3 position_ws;\n\
    out vec3 normal_ws;\n\
    out vec2 tex_coord;\n\
	\
    layout(std140) uniform Matrices {\n\
        mat4 mvp_matrix;\n\
        mat4 model_matrix;\n\
    } matrices;\n\
	\
    void main() {\n\
        gl_Position = matrices.mvp_matrix*vec4(vertex_position, 1.f);\n\
        vec4 tmp_position_ws = matrices.model_matrix*vec4(vertex_position, 1.f);\n\
        position_ws = tmp_position_ws.xyz/tmp_position_ws.w;\n\
        normal_ws = mat3x3(matrices.model_matrix)*vertex_normal;\n\
        tex_coord = vertex_tex_coord;\n\
    }";

var fragment_shader = "#version 300 es\n\
    precision mediump float;\n\
	\
    struct PointLight {\n\
       vec3 position_ws;\n\
       float r;\n\
       vec3 color;\n\
    };\n\
	\
    in vec3 position_ws;\n\
    in vec3 normal_ws;\n\
    in vec2 tex_coord;\n\
    out vec4 vFragColor;\n\
	uniform sampler2D color_tex;\n\
	\
    layout(std140) uniform CamInfo {\n\
       vec3 cam_pos_ws;\n\
    } additional_data;\n\
	\
    layout(std140) uniform Material {\n\
       vec3 color;\n\
       float specular_intensity;\n\
       float specular_power;\n\
    } material;\n\
	\
    layout(std140) uniform PointLights {\n\
		float lightNum;\n\
       PointLight lights[" + max_lights + "];\n\
    } point_lights;\n\
	\
    layout(std140) uniform AmbientLight {\n\
       vec3 color;\n\
    } ambient_light;\n\
	\
    layout(std140) uniform DirectionalLight {\n\
       vec3 color;\n\
       vec3 direction;\n\
    } directional_light;\n\
	\
    layout(std140) uniform ProceduralTexture {\n\
       float isit;\n\
    } proctex;\n\
	\
    void main() {\n\
		vec3 diffuse = vec3(0);\n\
		vec3 specular = vec3(0);\n\
		vec3 N = normalize(normal_ws);\n\
		vec3 V = normalize(additional_data.cam_pos_ws - position_ws);\n\
		\n\
		for (int i = 0; i<int(point_lights.lightNum); i++) {\n\
			PointLight point_light = point_lights.lights[i];\n\
			vec3 Ll = point_light.position_ws - position_ws;\n\
			float r = length(Ll);\n\
			float intensity = 1.f - r / point_light.r;\n\
			intensity *= intensity;\n\
			vec3 L = normalize(Ll);\n\
			float NdL = dot(N, L);\n\
			diffuse += clamp(NdL * intensity * point_light.color * material.color * (1.f - min(floor(r / point_light.r), 1.f)), 0.f, 1.f);\n\
			\n\
			vec3 H = normalize(L+V);\n\
			specular += pow(max(dot(N, H), 0.f), material.specular_power) * material.specular_intensity * point_light.color * intensity;\n\
		}\n\
		\
		diffuse +=  ambient_light.color;\n\
		diffuse +=  clamp(dot(N, normalize(directional_light.direction)) * directional_light.color, 0.f, 1.f);\n\
		vec3 H = normalize(directional_light.direction+V);\n\
		specular += pow(max(dot(N, H), 0.f), material.specular_power) * material.specular_intensity * directional_light.color;\n\
		\
		vec3 tex = vec3(0);\n\
		tex += texture(color_tex, tex_coord).rgb * (1.f - proctex.isit);\n\
		tex += vec3(fract(sin(dot(tex_coord, vec2(12.9898,78.233))) * 43758.5453 * length(additional_data.cam_pos_ws))) * material.color * proctex.isit;\n\
		\
		vFragColor = vec4(clamp(diffuse * tex  + specular, 0.f, 1.f), 1.0);\n\
    }";

main();

/*

2 kwadrat z texturami 
oswietlenie z gory 
textury 
piramid z teksturo
3 marca 
*/