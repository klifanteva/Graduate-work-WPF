   M S B A M L   `   `   `  ���� E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  %    WPFArchive.View.SetOfDocsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WPFArchive.View     8	viewmodel"clr-namespace:WPFArchive.ViewModel       � Title$  SetOfDocsWindow��$	��504��$	��800��.��� $	��457��$	��Top=�.+��� $
��Left=�$��23��$��177,120,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��495������ *	NameOfSet+�� $��Сохранить��$
��Left=�$��657,406,0,0q�$	��Top=�$	��102��$��32��
����İ����� *SaveCommand+�� -    ��Name$ label��$��Организация��$
��Left=�$��	10,10,0,0q�$	��Top=�$
��Bold"��� $��Номер описи��$
��Left=�$��	78,81,0,0q�$	��Top=�$
��Bold"��� $
��Left=�$��23��$��
177,85,0,0q�$
 Wrap=�$	��Top=�$	��120������ *NumberOfSet+�� $��Название��$
��Left=�$��
99,117,0,0q�$	��Top=�$
��Bold"��� $1��+Количество экземпляров��$
��Left=�$��
13,151,0,0q�$	��Top=�$
��Bold"��� $!��Начальная дата��$
��Left=�$��
66,189,0,0q�$	��Top=�$
��Bold"��� $��Конечная дата��$
��Left=�$��
72,229,0,0q�$	��Top=�$
��Bold"��� $��Категория��$
��Left=�$��
89,274,0,0q�$	��Top=�$
��Bold"��� $��Доступ��$
��Left=�$��457,199,0,0q�$	��Top=�$
��Bold"��� $
��Left=�$��23��$��177,155,0,0q�$
 Wrap=�$	��Top=�$	��141������ *Copies+(  "System.Windows.Controls.DatePicker  $
��Left=�$��177,191,0,0q�$	��Top=�$	��141��   SelectedDate �� *	StartDate+  $
��Left=�$��177,231,0,0q�$	��Top=�$	��141�� �� *	EndDate+�� $
��Left=�$	��120��$��514,241,0,0q�$
 Wrap=�$	��Top=�$	��228������ *ReasonOfClosedAccess+��  �� DisplayMemberPath$ nameOrg�� �� SelectedValuePath$ idOrg��$
��Left=�$��
102,14,0,0q�$	��Top=�$	��640������ *Organizations+ �� SelectedValue �� *IdOrg+�� $ nameCatOfSet��$ 
idCatOfSet��$
��Left=�$��177,278,0,0q�$	��Top=�$	��141������ *
Categories+ �� *
Category+�� $ 
nameAccess��$ idAccess��$
��Left=�$��514,203,0,0q�$	��Top=�$	��141������ *
Accesses+ �� *Access+�� -    ��Name$ 	textBlock��$
��Left=�$��420,242,0,0q�$
��Wrap=�$:��4Причина органичения доступа��$	��Top=�$��89��$
��Bold"�$��RightToLeft=�