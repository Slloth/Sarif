   M S B A M L   `   `   `  ����@  <Sarif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SarifSarif  5          
Sarif.shop   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:Sarif     5        N� Title$
  Shop��5	       N� 
Background 5
      �� 5   
    �� Color$ White��6    �� Opacity$
 0.25Q�6(   .��5      � -   5        .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ productDataGrid��5         AutoGenerateColumns .     EnableRowVirtualization . $��82,25,106.8,335.6q�6g      
IsReadOnly .    CanUserResizeColumns .  	   CanUserResizeRows	 .  
   CanUserReorderColumns
 .  
��� ���
��� ���$��Georgia)�6B     AlternatingRowBackground �	#FF525252   AreRowDetailsFrozen .    RowBackground �	#FF0E0E0E   HorizontalGridLinesBrush �	#FFFFD769   VerticalGridLinesBrush �	#FFFFD769   HeadersVisibility$ Column=�6�   � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� BasedOn �� 
TargetType �� Property �� Value��%�   '        7 �� 5      8   "  6Q   .�5      ��  � ��
 �����63   �� 5       � ��$ Arial)�63   �� 5       � ��
 � ���64   �� 5       � ��$ 0,0,1,1q�68   5      5      ��5      �� 5      $ 	#FFFFD775��6"   $
 0.23Q�64      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        
nameColumn��,  &System.Windows.Controls.DataGridColumn   Header$  Nom du Produit��5   R      Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6k   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *name+6k     5      -   5        priceColumn��$  Prix du Produit (€)��5   T   $	 120 6r    6:   �� *price+6r     5      -   5        categoriesColumn��$  Catégorie du Produit��5   h   $	 125 6�    6?   �� *categories.categorie+6�     5      -   5        numberColumn��$ 
 Quantité��5   V   $ 60 6i    6;   �� *number+6i     5      -   5        descriptionColumn��$  Description du Produit��5   `   $ * 6�    6@   �� *description+6�     5       -   5        serialNumberColumn��$  N° de serie du Produit��5    b   $ * 6�    6A   �� *serialNumber+6�   5"      �� 5#   
   $
��Left=�6   $	��378��6+   $��235,272,0,-24.4q�68   $	��Top=�6Q   $	��532��6i   $!��/Img/apercu_produit_try.png��6u   �� 5$   
   -	   5         ��Name$ previewProduct��5$      $��368,390,375.8,32.6q�6(   ��6D   �� *img+5%      5&      