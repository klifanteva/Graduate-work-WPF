   M S B A M L   `   `   `  ����E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  5   	   ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  65   !    WPFArchive.View.EventWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WPFArchive.View     5   	   8	viewmodel"clr-namespace:WPFArchive.ViewModel     65   @
extensions)using:Microsoft.Toolkit.Uwp.UI.Extensions     5        � Title$  EventWindow��5	   	   $	��490��6   $	��565��6*   .��5
      � $��0,0,0,-1q�6    � RowDefinitions 5   
   �� 5      �� 5      $��0* �6   .+�5   
   �� $��Статус��6   $
��Left=�6!   $��128,113,0,0q�6<   $	��Top=�6Q   $��26��6i   $��59��6u   $
��Bold"�6�   �� 5   
   $��Организация��6   $
��Left=�6&   $��	41,19,0,0q�6A   $	��Top=�6T   $
��Bold"�6l   $��26��6~   $��87��6�   �� 5   
   -   5         ��Name$ nameOrg_textBox��5      $
��Left=�6+   $��60��6F   $��
128,23,0,0q�6R    �� TextWrapping$
 Wrap=�6f   $	��Top=�6�   $	��333��6�   {�.  ��6z   �� *	NameOrg+6�   �� 5   
   $#��Тип мероприятия��6   $
��Left=�6*   $��
69,157,0,0q�6E   $	��Top=�6Y   $��26��6q   $	��118��6}   $
��Bold"�6�   �� 5   
   $#��Дата проведения��6   $
��Left=�6*   $��
69,198,0,0q�6E   $	��Top=�6Y   $��26��6q   $	��118��6}   $
��Bold"�6�   �� 5   
   $'��!Ответсвенное лицо��6   $
��Left=�6,   $��
59,241,0,0q�6G   $	��Top=�6[   $��26��6s   $	��128��6   $
��Bold"�6�   �� 5   
   $��Результат��6   $
��Left=�6$   $��117,284,0,0q�6?   $	��Top=�6T   $��26��6l   $��70��6x   $
��Bold"�6�   �� 5   
   $
��Left=�66   $��23��6Q   $��192,245,0,0q�6]   $
 Wrap=�6r   $	��Top=�6�   $	��203��6�   ��6   �� *ResponsibleOfEvent+6�   �� 5   
   $
��Left=�6*   $��61��6E   $��192,287,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $	��291��6�   ��6   �� *Result+6�   �� 5   
    �� DisplayMemberPath$ nameTypeOfEvent��68    �� SelectedValuePath$ idTypeOfEvent��6\   $
��Left=�6�   $��192,161,0,0q�6�   $	��Top=�6�   $	��203��6�   $��22��6�   ��6   �� *TypesOfEvent+ �� SelectedValue 6~   �� *IdTypeOfEvent+6�   (  "System.Windows.Controls.DatePicker  5   
   $
��Left=�6:   $��192,200,0,0q�6U   $	��Top=�6j   $	��145��6�   $��24��6�      SelectedDate 6   �� *DateOfEvent+6�   �� 5   
   $��Сохранить��66   $
��Left=�6J   $��393,385,0,0q�6e   $	��Top=�6z   $	��115��6�   $��36��6�   
����İ���6   �� *SaveEventCommand+6�   �� 5   
   $��Проведено��6.   $
��Left=�6B   $��192,119,0,0q�6]   $	��Top=�6r   $��15��6�   $��82��6�    d� 	IsChecked 6   �� *Done+6�   5      5      