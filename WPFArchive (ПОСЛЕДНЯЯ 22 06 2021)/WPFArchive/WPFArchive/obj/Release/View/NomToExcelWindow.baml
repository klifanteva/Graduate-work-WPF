   M S B A M L   `   `   `  ���� E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  &     WPFArchive.View.NomToExcelWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WPFArchive.View     8	viewmodel"clr-namespace:WPFArchive.ViewModel       � Title$8  2Выгрузка номенклатуры в Excel��$	��243��$��494.5��.��� .+��� -    ��Name$ textBox��$
��Left=�$��23��$��
142,65,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120������ *	YearNom+�� -    ��Name$ 	textBlock��$
��Left=�$��
117,66,0,0q�$
��Wrap=�$��Год��$	��Top=�$
��Bold"� J� RenderTransformOrigin$ 2.825,0.376&��� -   $ 
textBlock1��$
��Left=�$��	-5,98,0,0q�$
��Wrap=�$3��-Источник комплектования��$	��Top=�$
��Bold"�$	��142�� �� TextAlignment$ Right=��� -    ��Name$ comboBox�� �� DisplayMemberPath$ nameOrg�� �� SelectedValuePath$ idOrg��$
��Left=�$��142,108,0,0q�$	��Top=�$	��322������ *Organizations+	 �� SelectedValue	 �� *IdOrg+�� -   
 ��Name$
 button��$#��Создать Excel-файл��$
��Left=�$��350,154,0,0q�$	��Top=�$	��114��$��38��
����İ����� *ToExcelCommand+�� -   $ 
textBlock2��$
��Left=�$��	63,22,0,0q�$
��Wrap=�$]��WВведите год и выберите источник комплектования��$	��Top=�