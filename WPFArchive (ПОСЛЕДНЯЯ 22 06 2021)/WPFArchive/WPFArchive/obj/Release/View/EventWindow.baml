   M S B A M L   `   `   `  ���� E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  !    WPFArchive.View.EventWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WPFArchive.View     8	viewmodel"clr-namespace:WPFArchive.ViewModel     @
extensions)using:Microsoft.Toolkit.Uwp.UI.Extensions       � Title$  EventWindow��$	��490��$	��565��.��� $��0,0,0,-1q� � RowDefinitions �� �� $��0* �.+��� $��Статус��$
��Left=�$��128,113,0,0q�$	��Top=�$��26��$��59��$
��Bold"��� $��Организация��$
��Left=�$��	41,19,0,0q�$	��Top=�$
��Bold"�$��26��$��87���� -    ��Name$ nameOrg_textBox��$
��Left=�$��60��$��
128,23,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��333��{�.  ���� *	NameOrg+�� $#��Тип мероприятия��$
��Left=�$��
69,157,0,0q�$	��Top=�$��26��$	��118��$
��Bold"��� $#��Дата проведения��$
��Left=�$��
69,198,0,0q�$	��Top=�$��26��$	��118��$
��Bold"��� $'��!Ответсвенное лицо��$
��Left=�$��
59,241,0,0q�$	��Top=�$��26��$	��128��$
��Bold"��� $��Результат��$
��Left=�$��117,284,0,0q�$	��Top=�$��26��$��70��$
��Bold"��� $
��Left=�$��23��$��192,245,0,0q�$
 Wrap=�$	��Top=�$	��203������ *ResponsibleOfEvent+�� $
��Left=�$��61��$��192,287,0,0q�$
 Wrap=�$	��Top=�$	��291������ *Result+��  �� DisplayMemberPath$ nameTypeOfEvent�� �� SelectedValuePath$ idTypeOfEvent��$
��Left=�$��192,161,0,0q�$	��Top=�$	��203��$��22������ *TypesOfEvent+ �� SelectedValue �� *IdTypeOfEvent+(  "System.Windows.Controls.DatePicker  $
��Left=�$��192,200,0,0q�$	��Top=�$	��145��$��24��   SelectedDate �� *DateOfEvent+�� $��Сохранить��$
��Left=�$��393,385,0,0q�$	��Top=�$	��115��$��36��
����İ����� *SaveEventCommand+�� $��Проведено��$
��Left=�$��192,119,0,0q�$	��Top=�$��15��$��82�� d� 	IsChecked �� *Done+