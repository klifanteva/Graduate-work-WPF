   M S B A M L   `   `   `  ���� E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel       WPFArchive.View.DocsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WPFArchive.View     8	viewmodel"clr-namespace:WPFArchive.ViewModel     -     � Title$  
DocsWindow��$��556.3��$��528.5��.��� .+��� $6��0Добавить документ в опись:��$
��Left=�$��	10,10,0,0q�$	��Top=��� 
���   �$��1q�$
��Left=�$��35��$��	10,41,0,0q�$	��Top=�$	��500��._��� $
��Left=�$
��Wrap=�$��
-1,-1,-1,0q�$	��500��$��34��$	��Top=����� *	SetOfDocs+�� $#��Номер документа��$
��Left=�$��
26,136,0,0q�$	��Top=�$
��Bold"��� $'��!Дата формирования��$
��Left=�$��
12,240,0,0q�$	��Top=�$
��Bold"��� $��Тип документа��$
��Left=�$��
44,282,0,0q�$	��Top=�$
��Bold"��� $
��Left=�$��23��$��140,139,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120������ *	NumberDoc+(  "System.Windows.Controls.DatePicker  -     Name$ date_datePicker��$
��Left=�$��140,242,0,0q�$	��Top=�$	��155��   SelectedDate �� *	DateDoc+�� $
��Left=�$��140,286,0,0q�$	��Top=�$	��155�� �� DisplayMemberPath$ nameDocType�� �� SelectedValuePath$ 	idDocType������ *
DocTypes+ �� SelectedValue �� *	DocType+�� $
��Left=�$��140,373,0,0q�$	��Top=�$	��131��$��42��
�����F����� *SaveFileCommand+.���� -    ��Name$ saveFileButton��$
��Wrap=�$.��(Прикрепить новый файл�� �� TextAlignment$ Center=��� $��Сохранить��$
��Left=�$��354,464,0,0q�$	��Top=�$	��122��$��42��
����İ����� *SaveCommand+�� $��Номенклатура��$
��Left=�$��
35,196,0,0q�$	��Top=�$
��Bold"��� $ idNom��$
��Left=�$��140,200,0,0q�$	��Top=�$	��350������ *Nomenclatures+ �� *Nomenclature+���� .R��� ��`� 	 �� StringFormat$	 {0} {1}��.1��� 
 �� Path$
 actIndex��� $
 
actHeading��� -   $ 	textBlock��$
��Left=�$��140,187,-37,0q�$
��Wrap=�$���}номенклатура выведена в соотвествии с ИК и годом, указанными в описи��$	��Top=�$	��417��$��10(��� -    ��Name$ textBox��$
��Left=�$��23��$��140,330,0,0q�$
 Wrap=�$	��Top=�$	��120������ *SheetsCount+�� -   $ 
textBlock1��$
��Left=�$��
64,321,0,0q�$
��Wrap=�$'��!Количество листов��$	��Top=�$��78��$
��Bold"��� -   $ fileExsists��$
��Left=�$��283,372,0,0q�$
��Wrap=�$W��Qдля этого документа еще не был добавлен файл��$	��Top=� J� RenderTransformOrigin$ 0.5,0.5&�$	��124��$��10(�$��43��$ Center=�z�V� .~��� ��  �� AngleX$ -0.001Q���  �� Angle$ -0.009Q�T� �� -    ��Name$ deleteFileButton��$��Удалить файл��
�����F�$
��Left=�$��140,431,0,0q�$	��Top=�$	��131��$��36��$y�Hidden=����� *DeleteFileCommand+