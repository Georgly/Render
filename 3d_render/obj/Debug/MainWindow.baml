   M S B A M L   `   `   `  ����D  @3d_render, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:_3d_render
_3d_render  5   	       _3d_render.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:_3d_render     5   	   -   5          �Name$  
_3D_Render��5   	    � Title$ 	3D Render��5   	   $	��950��6   $
��1225��6+   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      $
��auto �6   6.   .+�5   
   �� -   5        $��myCanva��5   (   
����ح�6Y   �� 5   
   $��1��6   .+�5      �� -   5         ��Name$ 
openFileBt��5      $��5q�6)   $��2q�64   $��Arial)�6H   .��6t   Выбрать файл6�   �� 5      -   5        $��drawModelBt��5      $��5q�6(   $��2q�63   $��Arial)�6G   .��6t   #!Нарисовать модель6�   ]� 5      .+�5      �� -   5        $
��upBt��5      $	��Top=�6%   .��6M   Up6Q   �� 5      -   5        $��downBt��5      $��Bottom=�6'   .��6T   Down6Z   �� 5      -   5        $��leftBt��5      $
��Left=�6'   .��6R   Left6X   �� 5      -   5        $��rightBt��5      $��Right=�6(   .��6U   Right6\   �� 5      
�������{�.  6@   5      �� 5      -	   5        $��turnBt��5   *   $��0, 10, 0, 0q�6   .��6M   Turn6S   �� 5      -
   5        $��	turnCoord��5      .+�5      �� -   5        $��xCoord��5      .��6)   X6,   �� 5      -   5        $��yCoord��5      .��6)   Y6,   �� 5      -   5        $��zCoord��5      .��6)   Z6,   5      �� 5      -   5        $��
zoomSlider��5   )   $��
2, 5, 2, 5q�6    � Maximum$
 1000Q�6;    � Minimum$ 0Q�6J    � Value$ 2Q�6V   6b   5       5!      5"      