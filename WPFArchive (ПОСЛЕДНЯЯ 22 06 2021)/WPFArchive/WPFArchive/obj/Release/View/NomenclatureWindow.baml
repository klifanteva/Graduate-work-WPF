   M S B A M L   `   `   `  ���� E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  (    "WPFArchive.View.NomenclatureWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WPFArchive.View     8	viewmodel"clr-namespace:WPFArchive.ViewModel     $��754.5��$
��1000��  � Title$%  Номенклатура дел�� � WindowState$ 	Maximized=�.���  � ColumnDefinitions �� $��0* ��� .+�&   System.Windows.Controls.DataGrid  -     Name$ nom_dataGrid��$	��390��$��
6,132,11,0q�$	��Top=�   AutoGenerateColumns .    
IsReadOnly . $��1������ *NomenclaturesForDataGrid+ �� SelectedItem �� *SelectedNom+�� -    ��Name$ 	textBlock��$
��Left=�$��
40,0,0,161q�$
��Wrap=�$��Индекс дела��$��Bottom=�$
��Bold"� �� TextAlignment$ Right=�$��1��$��16��$��74���� -   $ 
textBlock1��$
��Left=�$��
22,0,0,119q�$
��Wrap=�$!��Заголовок дела��$��Bottom=�$
��Bold"�$ Right=�$��1��$��16��$��92���� -   $ 
textBlock2��$
��Left=�$��	94,0,0,81q�$
��Wrap=�$��Год��$��Bottom=�$
��Bold"�	 J� RenderTransformOrigin$	 1.866,0.501&�$��1��$��16��$��20���� -   $ 
textBlock3��$
��Left=�$��	12,0,0,33q�$
��Wrap=�$3��-Источник комплектования��$��Bottom=�$	��102��$
��Bold"�$ Right=�$��1��$��32���� -   
 ��Name$
 actIndex_textBox��$
��Left=�$��24��$��119,0,0,154q� �� TextWrapping$
 Wrap=�$��Bottom=�$	��120��$��1������ *
ActIndex+�� -   $
 actHeading_textBox��$
��Left=�$��38��$��119,0,0,107q�$
 Wrap=�$��Bottom=�$	��616��$��1������ *
ActHeading+�� -    ��Name$ org_comboBox�� �� DisplayMemberPath$ nameOrg�� �� SelectedValuePath$ idOrg��$
��Left=�$��
119,0,0,33q�$��Bottom=�$	��616��$��32��$��1������ *Organizations+ �� SelectedValue �� *IdOrg+�� -	   $
 yearNom_textBox��$
��Left=�$��22��$��
119,0,0,75q�$
 Wrap=�$��Bottom=�$	��120��$��1������ *	YearNom+�� -
    ��Name$ save_Button��$��Right=�$��
0,0,29,131q�$��Bottom=�$	��128��$��38��
����İ�$��1������ *SaveCommand+.���� -   $ saveButtonContent��$��Добавить��$
��Wrap=�$ Center=��� -   $ button_Copy��$��Right=�$��	0,0,29,27q�$��Bottom=�$	��128��$��38��
����İ�$��1������ *DeleteNomCommand+.���� $)��#Удалить выделенное��$
��Wrap=�$ Center=��� -   $ removeSelection_Button��$%��Убрать выделение��$��Right=�$��	0,0,29,72q�$��Bottom=�$	��128��$��33��
�����F�$��1���� -   $ toExcel��$��1��$��Right=�$��	0,12,10,0q�$	��Top=�$	��159��$��47��
�����F�.���� -   $ 
textBlock4��$
��Wrap=�$,��&Выгрузка списка в Excel��$ Center=��� -   $
 textBox��$
��Left=�$��24��$��
116,35,0,0q�$
 Wrap=�$	��Top=�$	��398��$��1������ *SearchByHeading+�� -   $
 textBox_Copy��$
��Left=�$��22��$��
116,70,0,0q�$
 Wrap=�$	��Top=�$	��398��$��1������ *SearchByOrg+�� -   $
 textBox_Copy1��$
��Left=�$��23��$��116,101,0,0q�$
 Wrap=�$	��Top=�$��84��$��1������ *SearchByYear+�� -   $ 
textBlock5��$
��Left=�$��6,10,0,0q�$
��Wrap=�$��
Поиск��$	��Top=�$��46��$
��Bold"�$ Right=�$��1��$��16���� -   $ 
textBlock6��$
��Left=�$��3,36,0,0q�$
��Wrap=�$&�� по заголовку дела��$	��Top=�$	��102��$��1��$��16���� -   $ textBlock6_Copy��$
��Left=�$��3,70,0,0q�$
��Wrap=�$��	по ИК��$	��Top=�$	��102��$ Right=�$��1��$��16���� -   $ textBlock6_Copy1��$
��Left=�$��	3,102,0,0q�$
��Wrap=�$��по году��$	��Top=�$	��102��$ Right=�$��1��$��16���� -   $ button_Copy2��$��
Поиск��$
��Left=�$��
542,70,0,0q�$	��Top=�$��84��$��22��
�����F�$��1������ *SearchCommand+