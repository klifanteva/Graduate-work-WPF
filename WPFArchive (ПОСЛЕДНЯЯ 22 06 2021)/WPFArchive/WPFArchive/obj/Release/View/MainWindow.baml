   M S B A M L   `   `   `  ���� E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:WPFArchive
WPFArchive  ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel      WPFArchive.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:WPFArchive     8	viewmodel"clr-namespace:WPFArchive.ViewModel       � Title$�  �Информационная система для работы с источниками комплектования и документами��$	��598��$
��1347�� � WindowState$ 	Maximized=�.���  � ColumnDefinitions �� $	��200 ��� .+�� -    �Name$ setOfDocsPanel��$
��10,0q�$��1��
�������.+�&   System.Windows.Controls.DataGrid  -     Name$ setOfDocsDataGrid��$��	0,201,0,0q�$��Right=�$��12(�   AutoGenerateColumns . $��Stretch=�   
IsReadOnly . ���� *	SetOfDocs+ �� SelectedItem �� *SelectedSet+����  �� 
TargetType)  #System.Windows.Controls.DataGridRow"  .�-   �� -   	 ��Name$	 addSetButton��$��Right=�$��	0,10,10,0q�$	��Top=�$	��132��$��42��
����İ�.���� $,��&Добавить новую опись��$
��Wrap=�
 �� TextAlignment$
 Center=��� -   $	 deleteSetButton��$��Right=�$��	0,64,10,0q�$	��Top=�$	��132��$��40��
����İ����� *DeleteSetCommand+.���� $2��,Удалить выбранную опись��$
��Wrap=�$
 Center=�� -   $ filtersForSet��$
��Left=�$	��186��$��	10,10,0,0q�$	��Top=�$	��668��.+��� -    ��Name$ searchOrg_textBox1��$
��Left=�$��23��$��
164,28,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��316������ *SearchByNameForSet+�� -    ��Name$ textBloc��$
��Left=�$��
164,14,0,0q�$
��Wrap=�$U��Oвведите название организации или его часть��$	��Top=�$	��316��$��14��$��10(�$
 Center=�(  "System.Windows.Controls.DatePicker  $
��Left=�$��
165,89,0,0q�$	��Top=�$	��134��   SelectedDate �� *StartFilterDate+  $
��Left=�$��
344,89,0,0q�$	��Top=�$	��131�� �� *EndFilterDate+�� -	   $ 
textBlock2��$
��Left=�$��
165,73,0,0q�$
��Wrap=�$!��начало периода��$	��Top=�$	��134��$
 Center=��� -
   $ 
textBlock3��$
��Left=�$��
344,73,0,0q�$
��Wrap=�$'��!окончание периода��$	��Top=�$
 Center=�$	��131���� -   $ 
textBlock5��$
��Left=�$��
17,141,0,0q�$
��Wrap=�$*��$Фильтр по категории��$	��Top=�$
��Bold"�$	��133��$
 Right=��� -    ��Name$ checkBox��$:��4опись дел по личному составу��$
��Left=�$��165,134,0,0q�$	��Top=�$	��193�� d� 	IsChecked �� *CatPersFilter+�� -   $ 	checkBox1��$?��9опись дел постоянного хранения��$
��Left=�$��165,154,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.31,0.6&�$	��212�� �� *CatStoreFilter+�� -   $	 button2��$
��Left=�$��
570,77,0,0q�$	��Top=�$��89��
�����F����� *FilterCommandForSet+.���� -   $ 
textBlock7��$
��Wrap=�$'��!Применить фильтры��$
 Center=��� -   $ 
textBlock4��$
��Left=�$��	17,22,0,0q�$
��Wrap=�$=��7Поиск по названию организации��$	��Top=�$	��128��$
 Right=�$
��Bold"��� -   $ 
textBlock6��$
��Left=�$��	17,81,0,0q�$
��Wrap=�$>��8Фильтр по начальной дате описи��$	��Top=�$
��Bold"�$
 Right=�$	��133��� -   $ orgPanel��$
��10,0q�$��1��.+�  -   $ orgDataGrid��$��	0,200,0,0q�$��Right=� . $��Stretch=� . ���� *Organizations+ �� *SelectedOrg+���� "  .�-   ��  �� Property � �� �� Value$
 Wrap=��� -   $	 addOrgButton��$��Right=�$��	0,10,10,0q�$	��Top=�$	��132��$��42��
����İ�.���� $8��2Добавить новую организацию��$
��Wrap=�$
 Center=��� -   $	 deleteOrgButton��$��Right=�$��	0,63,10,0q�$	��Top=�$	��132��$��42��
����İ����� *DeleteOrgCommand+.���� $>��8Удалить выбранную организацию��$
��Wrap=�$
 Center=��� -   $	 button��$��Right=�$��
0,10,147,0q�$	��Top=�$	��132��$��42��
�����F����� *ToExcelCommand+.���� $7��1Выгрузить весь список в Excel��$
��Wrap=�$
 Center=�$��Right=�$��Bottom=�$	��107��� -   $��filtersForOrg��$
��Left=�$	��185��$��	10,10,0,0q�$	��Top=�$	��669��.+��� -   $ searchOrg_textBox��$
��Left=�$��22��$��
155,39,0,0q�$
 Wrap=�$	��Top=�$	��316������ *SearchByNameForOrg+�� -   $ 
textBlock1��$
��Left=�$��
155,25,0,0q�$
��Wrap=�$U��Oвведите название организации или его часть��$	��Top=�$	��316��$��14��$
 Center=�$��10(��� -   $ munOwnForm_CheckBox��$ ��муниципальная��$
��Left=�$��
155,95,0,0q�$	��Top=�$��16��$	��107�� �� *MunFilterOwnForm+�� -   $ 	textBlock��$
��Left=�$��	10,96,0,0q�$
��Wrap=�$=��7Фильтр по форме собственности��$	��Top=�$
��Bold"�$��35��$	��130��$
 Right=��� $��областная��$
��Left=�$��155,115,0,0q�$	��Top=�$��16��$��76�� �� *OblFilterOwnForm+�� $��федеральная��$
��Left=�$��
295,95,0,0q�$	��Top=�$��16��$��92�� �� *FedFilterOwnForm+�� $(��"негосударственная��$
��Left=�$��295,115,0,0q�$	��Top=�$��16��$	��125�� �� *NotGosFilterOwnForm+�� -   $	 searchOrgButton��$��	0,0,10,72q�$��Bottom=�$��89��
�����F�$��36��$��Right=����� *FilterCommandForOrg+.���� $'��!Применить фильтры��$
��Wrap=�$
 Center=�$��Right=�$��Bottom=��� -   $ textBlock4_Copy��$
��Left=�$��	12,33,0,0q�$
��Wrap=�$=��7Поиск по названию организации��$	��Top=�$	��128��$
 Right=�$
��Bold"�� $��0�� J� OpacityMask
 �   ����	#FFE0E0E0.+��� -   $	 setOfDocs_Button��$��40��
����İ�$$��Работа с описями��$��
10,111,0,0q�$
��Left=�$	��Top=�$	��180���� -    $	 
org_Button��$��40��
����İ�$0��*Работа с организациями��$��	10,66,0,0q�$	��Top=�$
��Left=�$	��180���� -!   $	 settings_Button��$��40��
����İ�$��Настройки��$��10,517,0,10q�$
��Left=�$	��180��$��Bottom=��� -"   $	 nomenclature_Button��$��40��
����İ�$��Номенклатура��$��	10,21,0,0q�$	��Top=�$
��Left=�$	��180��