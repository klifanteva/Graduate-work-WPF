   M S B A M L   `   `   `  ����E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  5   	   ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  65   %    WPFArchive.View.SetOfDocsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WPFArchive.View     5   	   8	viewmodel"clr-namespace:WPFArchive.ViewModel     65     � Title$  SetOfDocsWindow��5	   	   $	��504��6!   $	��800��6.   .��5
      � $	��457��6   $	��Top=�6   .+�5   
   �� $
��Left=�6-   $��23��6H   $��177,120,0,0q�6T    �� TextWrapping$
 Wrap=�6i   $	��Top=�6}   $	��495��6�   ��6   �� *	NameOfSet+6�   �� 5   
   $��Сохранить��6   $
��Left=�6E   $��657,406,0,0q�6`   $	��Top=�6u   $	��102��6�   $��32��6�   
����İ���6%   �� *SaveCommand+6�   �� 5   
   -   5         ��Name$ label��5      $��Организация��6   $
��Left=�65   $��	10,10,0,0q�6P   $	��Top=�6c   $
��Bold"�6{   �� 5   
   $��Номер описи��6   $
��Left=�6&   $��	78,81,0,0q�6A   $	��Top=�6T   $
��Bold"�6l   �� 5   
   $
��Left=�6/   $��23��6J   $��
177,85,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $	��120��6�   ��6   �� *NumberOfSet+6�   �� 5   
   $��Название��6   $
��Left=�6#   $��
99,117,0,0q�6>   $	��Top=�6R   $
��Bold"�6j   �� 5   
   $1��+Количество экземпляров��6   $
��Left=�61   $��
13,151,0,0q�6L   $	��Top=�6`   $
��Bold"�6x   �� 5   
   $!��Начальная дата��6   $
��Left=�6)   $��
66,189,0,0q�6D   $	��Top=�6X   $
��Bold"�6p   �� 5   
   $��Конечная дата��6   $
��Left=�6(   $��
72,229,0,0q�6C   $	��Top=�6W   $
��Bold"�6o   �� 5   
   $��Категория��6   $
��Left=�6$   $��
89,274,0,0q�6?   $	��Top=�6S   $
��Bold"�6k   �� 5   
   $��Доступ��6   $
��Left=�6!   $��457,199,0,0q�6<   $	��Top=�6Q   $
��Bold"�6i   �� 5   
   $
��Left=�6*   $��23��6E   $��177,155,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $	��141��6�   ��6   �� *Copies+6�   (  "System.Windows.Controls.DatePicker  5   
   $
��Left=�68   $��177,191,0,0q�6S   $	��Top=�6h   $	��141��6�      SelectedDate 6   �� *	StartDate+6�     5   
   $
��Left=�66   $��177,231,0,0q�6Q   $	��Top=�6f   $	��141��6~    6   �� *	EndDate+6~   �� 5   
   $
��Left=�68   $	��120��6S   $��514,241,0,0q�6`   $
 Wrap=�6u   $	��Top=�6�   $	��228��6�   ��6   �� *ReasonOfClosedAccess+6�   �� 5   
    �� DisplayMemberPath$ nameOrg��69    �� SelectedValuePath$ idOrg��6U   $
��Left=�6�   $��
102,14,0,0q�6�   $	��Top=�6�   $	��640��6�   ��6   �� *Organizations+ �� SelectedValue 6o   �� *IdOrg+6�   �� 5   
   $ nameCatOfSet��66   $ 
idCatOfSet��6W   $
��Left=�6�   $��177,278,0,0q�6�   $	��Top=�6�   $	��141��6�   ��6   �� *
Categories+ 6v   �� *
Category+6�   �� 5   
   $ 
nameAccess��64   $ idAccess��6S   $
��Left=�6�   $��514,203,0,0q�6�   $	��Top=�6�   $	��141��6�   ��6   �� *
Accesses+ 6p   �� *Access+6�   �� 5   
   -   5         ��Name$ 	textBlock��5      $
��Left=�6'   $��420,242,0,0q�6B   $
��Wrap=�6W   $:��4Причина органичения доступа��6k   $	��Top=�6�   $��89��6�   $
��Bold"�6�   $��RightToLeft=�6�   5      5       