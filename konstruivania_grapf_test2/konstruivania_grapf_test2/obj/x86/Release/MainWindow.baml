   M S B A M L   `   `   `  ���� T  Pkonstruivania_grapf_test2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:konstruivania_grapf_test2konstruivania_grapf_test2  *    $konstruivania_grapf_test2.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9local'clr-namespace:konstruivania_grapf_test2     -     �Name$  main_window�� � Title$� �Програма знаходження найкоротшого шляху у графі за алгоритмом Дейкстри ��$	��431��$	��734�� � 
ResizeMode$ CanResizeWithGrip=� � Triggers :�  :� RoutedEvent$ FrameworkElement.Loaded������  �� EndPoint$ 0.226,-0.304&� �� 
StartPoint$ 0.004,0.984&�.���  � Color$ Black�� � Offset$ 0Q�� $ White��$ 1Q�.��� -   	 �Name$	 grid_main_window1��$	��394��$	��709��
 � ColumnDefinitions
 �� $
��163* ��� $
��192* �.+�A� $��50��$��50��$��1��$��4.5,172,329,172q�� -   $	 grid1��$	��389��$
��Left=�$��223.5,-1,0,0q�$	��Top=�$	��158��$��1������ $ 1.117,0.005&�$ 
0.06,0.959&�.��� $ Black��$ 0Q�� $ White��$ 1Q�.+��� -    ��Name$ button2��$%��Найкоротший шлях��$��32��$
��Left=�$��19,6,0,0q�$	��Top=�$	��125���� -    ��Name$ label1��$��з��$��25��$
��Left=�$��	10,50,0,0q�$	��Top=�$��15�� J� RenderTransformOrigin$ 0.667,-0.04&��� -    ��Name$ 	comboBox1��$��25��$
��Left=�$	��Top=�$��39�� �� Text$  �� �� SelectedIndex$ 0��$��	30,50,0,0q����	#FF7A2E2E���	#FF953232 J� OpacityMask �	#FFE42727���� $ 
0.958,0.44&�$ 	0.158,0.6&�.��� $ 	#FF41ACEF��$ 0Q�� $ White��$ 1Q��� -    ��Name$ 	checkBox1��$,��&Вкл\викл редагування��$��16��$
��Left=�$��5,75,0,0q�$	��Top=� d� 	IsChecked$
 TrueY��� -    ��Name$ 	groupBox2��$��Редагування��$	��214��$
��Left=�$��	10,92,0,0q�$	��Top=�$	��140��{�. .��� $	��258��.+��� -	   $ button3��$��Зберегти��$��33��$
��Left=�$��11,3,0,0q�$	��Top=�$	��111���� -
   $ button5��$��Очистити��$��39��$
��Left=�$��	11,42,0,0q�$	��Top=�$	��111���� -   $ button4��$��Завантажити��$��52��$
��Left=�$��	11,87,0,0q�$	��Top=�$	��111���� -   $ button1��$��Зєднати��$��44��$
��Left=�$��
11,145,0,0q�$	��Top=�$	��111��
 J� Uid$  ���� $
��Left=�$��	74,50,0,0q�$	��Top=�$��70��$��25��.@��� $��Roads��$	��100��$	��100���� $��Rivers��$	��100��$	��100��� -   $	 grid_main_window��$	��383��$
��Left=�$��0,5,0,0q�$	��Top=�$	��543��$��2������  �� ImageSource$6 0C:\Users\Artemka\Desktop\wedding.tree.spiral.png��(   "konstruivania_grapf_test2.btn_exit  (   "konstruivania_grapf_test2.btn_exit   	MouseDown$ btn_exit_MouseDown��   MouseDoubleClick$ btn_exit_MouseDoubleClick��$��53��$��0,0,8,18q�$��Bottom=�$��Right=�$	��140��$��1���� $��Button��$
��Left=�$��45��$��162,-195,0,0q�   ButtonStyle1#���   $	��Top=�$	��124��