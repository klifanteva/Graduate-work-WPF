   M S B A M L   `   `   `  ����E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  5   	   ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  65   "    WPFArchive.View.RenameWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WPFArchive.View     5   	   8	viewmodel"clr-namespace:WPFArchive.ViewModel     65     � Title$  RenameWindow��5	   	   $	��435��6   $	��565��6+   .��5
      � .+�5   
   �� -   5         ��Name$ nameOrg_textBox��5      $
��Left=�6+   $��59��6F   $��
141,43,0,0q�6R    �� TextWrapping$
 Wrap=�6f   $	��Top=�6�   $	��333��6�   ��6z   �� *	NameOrg+6�   �� 5   
   $
��Left=�6   $��	25,43,0,0q�6/   $
��Wrap=�6B   $<��6Текущее название организации��6V   $	��Top=�6z   $��37��6�   $	��111��6�   $
��Bold"�6�   �� 5   
   $
��Left=�6   $��
46,143,0,0q�6/   $
��Wrap=�6C   $+��%Предыдущее название��6W   $	��Top=�6r   $
��Bold"�6�   �� 5   
   $!��Начальная дата��6   $
��Left=�6)   $��
76,221,0,0q�6D   $	��Top=�6X   $
��Bold"�6p   �� 5   
   $��Конечная дата��6   $
��Left=�6(   $��
82,262,0,0q�6C   $	��Top=�6W   $
��Bold"�6o   �� 5   
   $
��Left=�6/   $��62��6J   $��197,142,0,0q�6V   $
 Wrap=�6k   $	��Top=�6   $	��318��6�   ��6   �� *NameRenamed+6�   (  "System.Windows.Controls.DatePicker  5   
   $
��Left=�68   $��197,221,0,0q�6S   $	��Top=�6h   $	��172��6�      SelectedDate 6   �� *	StartDate+6�     5   
   $
��Left=�66   $��197,264,0,0q�6Q   $	��Top=�6f   $	��172��6~    6   �� *	EndDate+6~   �� 5   
   $��Сохранить��61   $
��Left=�6E   $��400,339,0,0q�6`   $	��Top=�6u   $	��115��6�   $��36��6�   
����İ���6   �� *SaveCommand+6�   5      5      