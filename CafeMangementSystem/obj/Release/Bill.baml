   M S B A M L   `   `   `  ���� N  JCafeMangementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CafeMangementSystemCafeMangementSystem      CafeMangementSystem.Bill   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:CafeMangementSystem     -     � Title$
  Bill��$
��1000��$	��800�� � 	Resources    DGHeaderStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property  DGCHeaderStyle �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%
  &	        &	 3     �� "  .���  �@ ColumnHeaderStyle# �  �� "  .���  � ��
 �������  � ��
 �   ���  � ��
 �������  � ��$ 85����  � ��$ 0q���  � ��$
 Left=���  � ��$ 0����  � ��$ 30��.��� .+��� $��	Cafe Bill��$��298,30,298,864q�$��48(��� -   $��Print��$��700,20,21,908.5q�$��18(��� $!��1234 Testvej, Aarhus v 8210��$
��Left=�$��268,100,0,0q�$	��Top=�$��20(��� $��45+ 12 34 56 78��$
��Left=�$��314,130,0,0q�$	��Top=�$��20(��� $��www.Test.com��$
��Left=�$��323,160,0,0q�$	��Top=�$��20(�  -     Name$ DataGrid��   VerticalGridLinesBrush
 ����    HeadersVisibility$ Column=�#���   $��268,236,172,405q�
�������
�������   HorizontalGridLinesBrush
 ������� $	��VAT��$��220,580,526,356q�$��20(�	 J� RenderTransformOrigin$	 0.612,0.176&��� -   
 ��Name$
 VatBox��$��270,580,197,356q� �� TextWrapping$
 Wrap=�$��20(�