   M S B A M L   `   `   `  ���� E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  (    "WPFArchive.View.ContentOfSetWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WPFArchive.View     8	viewmodel"clr-namespace:WPFArchive.ViewModel       � Title$  Состав описи��$	��650��$
��1207�� � WindowState$ 	Maximized=�.��� .+�� $��	0,0,342,0q�.+�&   System.Windows.Controls.DataGrid  -     Name$ docsGrid��$��10,161,10,10q�$��Right=�$��Stretch=�   
IsReadOnly .    AutoGenerateColumns . ���� *	Documents+ �� SelectedItem �� *SelectedDocument+�� -    ��Name$ EditSet_Button��$��	0,19,10,0q�$	��Top=�$	��103��$��37��$��Center=�
�����F�$��Right=�$��Center=�.���� $+��%Редактировать опись��$
��Wrap=� �� TextAlignment$ Center=��� -   $ addDocToSet��$��
0,117,10,0q�$	��Top=�$	��103��$��37��$��Center=�
����İ�$��Right=�.���� $2��,Добавить новый документ��$
��Wrap=�$ Center=��� -    ��Name$ 	textBlock��$
��Left=�$��	10,10,0,0q�$
��Wrap=�$	��Top=�$
��Bold"�$	��721��$��40������ *	NameOfSet+�� -   $ 
textBlock3��$
��Left=�$��	10,58,0,0q�$
��Wrap=�$1��+Поиск по заголовку дела��$	��Top=�$
��Bold"��� -   	 ��Name$	 textBox��$
��Left=�$��23��$��
172,57,0,0q�
 �� TextWrapping$

 Wrap=�$	��Top=�$	��265������ *SearchByActHead+(  "System.Windows.Controls.DatePicker  $
��Left=�$��174,108,0,0q�$	��Top=�$	��128��   SelectedDate �� *StartFilterDate+  $
��Left=�$��324,108,0,0q�$	��Top=�$	��128�� �� *EndFilterDate+�� -   $ textBlock3_Copy��$
��Left=�$��
67,108,0,0q�$
��Wrap=�$ ��Фильтр по дате��$	��Top=�$
��Bold"��� -   $ button2��$
��Left=�$��
477,72,0,0q�$	��Top=�$��95��
�����F����� *FilterCommand+.���� -	   $ 
textBlock1��$
��Wrap=�$'��!Применить фильтры��$ Center=��� -
   $ 
textBlock2��$
��Left=�$��
174,92,0,0q�$
��Wrap=�$!��начало периода��$	��Top=�$	��128��$ Center=��� -   $ textBlock2_Copy��$
��Left=�$��
324,92,0,0q�$
��Wrap=�$'��!окончание периода��$	��Top=�$	��128��$ Center=��� -   $ EditSet_Button_Copy��$��	0,61,10,0q�$	��Top=�$	��103��$��37��$��Center=�
�����F�$��Right=�$��Center=����� *ToExcelCommand+.���� $9��3Выгрузить состав описи в Excel��$
��Wrap=�$ Center=�� $	��327�����	#FFE0E0E0$��
0,10,10,11q�$��Right=� � ColumnDefinitions �� $
��176* ��� $
��151* �.+��� $
��Left=�$��	10,30,0,0q�$
��Wrap=�$)��#Выбранный документ��$	��Top=��� -   $ docTextBlock��$
��Wrap=�$	��Top=�$��	10,51,0,0q�$
��Bold"�
�������$
��Left=�$	��307��$��86��$��2������ *
DocTextBox+�� -   $ EditDocument_Button��$
��Left=�$��
10,168,0,0q�$	��Top=�$	��156��$��43��
����İ�.���� $K��EРедактировать информацию о документе��$
��Wrap=�$ Center=��� $%��Удалить документ��$
��Left=�$��
10,216,0,0q�$	��Top=�$	��156��$��43��
����İ����� *DeleteDocCommand+�� $
��Left=�$��
22,168,0,0q�$	��Top=�$	��119��$��43��
�����F�$��1������ *OpenFileCommand+.���� $��Открыть файл��$
��Wrap=�$ Center=��� -   $ button��$��1��$
��Left=�$��
22,216,0,0q�$	��Top=�$	��119��$��43��
�����F����� *DeleteFileDocCommand+.���� -   $
 dfgh��$
��Wrap=�$0��*Удалить файл документа��$ Center=�