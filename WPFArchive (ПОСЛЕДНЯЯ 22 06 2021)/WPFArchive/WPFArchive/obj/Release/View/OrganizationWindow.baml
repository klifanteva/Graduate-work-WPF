   M S B A M L   `   `   `  ���� E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  (    "WPFArchive.View.OrganizationWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WPFArchive.View     8	viewmodel"clr-namespace:WPFArchive.ViewModel     @
extensions)using:Microsoft.Toolkit.Uwp.UI.Extensions     -     � Title$  OrganizationWindow��$��788.6��$	��890��.���  � ColumnDefinitions �� $
��173* ��� $
��564* ��� $
��145* �.+��� -    ��Name$ 
tabControl��$��0,80,0,0q�$��3��.@��� -    ��Name$ item1��$!��Общие сведения��$��22��$	��Top=�.��� -    �Name$ 
newOrgGrid��
�������$	��610�� �� $	��32* ��� $
��187* �.+��� -    ��Name$ label��$-��'Название организации��$
��Left=�$��	10,37,0,0q�$	��Top=�$
��Bold"�$��2���� -   $ label1��$%��Дата образования��$
��Left=�$��	36,85,0,0q�$	��Top=�$
��Bold"�$��2���� -   $ label2��$��
Адрес��$
��Left=�$��104,126,0,0q�$	��Top=�$
��Bold"�$��2���� -    ��Name$ nameOrg_textBox��$
��Left=�$��36��$��	37,37,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��649��$��1������ *	NameOrg+(  "System.Windows.Controls.DatePicker  -	     Name$ foundDate_datePicker��$
��Left=�$��	37,87,0,0q�$	��Top=�$	��169��$��1��	   SelectedDate	 �� *FoundationDate+�� -
   $ adress_textBox��$
��Left=�$��50��$��
37,126,0,0q�$
 Wrap=�$	��Top=�$	��420��$��1������ *Adress+�� 
�����F�$��1q�$
��Left=�$	��138��$��
65,190,0,0q�$	��Top=�$	��520��$��2��._�� .+��� -   $ label3��$��Руководитель��$
��Left=�$��	10,10,0,0q�$	��Top=�$
��Bold"��� -   $ label4��$��ФИО��$
��Left=�$��	59,34,0,0q�$	��Top=��� -   $ label5��$��Телефон��$
��Left=�$��	39,65,0,0q�$	��Top=��� -   $ label6��$��Email��$
��Left=�$��	58,96,0,0q�$	��Top=��� -   $ directorName_textBox��$
��Left=�$��23��$��
101,37,0,0q�$
 Wrap=�$	��Top=�$	��407������ *NameDirector+
 �� Mode$
 TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions . �� -   $ directorEmail_textBox��$
��Left=�$��23��$��
101,99,0,0q�$
 Wrap=�$	��Top=�$	��198������ *EmailDirector+$
 TwoWay=� .  . �� -   $ directorPhone_textBox��$
��Left=�$��23��$��
101,68,0,0q�$
 Wrap=�$	��Top=�$	��198�� �� 	MaxLength �� *MaxPhoneLength+���� *PhoneDirector+$
 TwoWay=� .  . �� 
�����F�$��1q�$
��Left=�$	��138��$��
65,341,0,0q�$	��Top=�$	��520��$��2��._�� .+��� -   $ label8��$0��*Ответственный за архив��$
��Left=�$��	10,10,0,0q�$	��Top=�$
��Bold"��� -   $ label41��$��ФИО��$
��Left=�$��	59,34,0,0q�$	��Top=��� -   $ label51��$��Телефон��$
��Left=�$��	39,65,0,0q�$	��Top=��� -   $ label61��$��Email��$
��Left=�$��	58,96,0,0q�$	��Top=��� -   $ respPersonName_textBox��$
��Left=�$��23��$��
101,37,0,0q�$
 Wrap=�$	��Top=�$	��407������ *NameRespPers+$
 TwoWay=� .  . �� -   $ respPersonEmail_textBox��$
��Left=�$��23��$��
101,99,0,0q�$
 Wrap=�$	��Top=�$	��198������ *EmailRespPers+$
 TwoWay=� .  . �� -   $ respPersPhone_textBox��$
��Left=�$��23��$��
101,68,0,0q�$
 Wrap=�$	��Top=�$	��198������ *PhoneRespPers+$
 TwoWay=� .  . �� -   $ label7��$��Куратор��$
��Left=�$��
97,496,0,0q�$	��Top=�$
��Bold"�$��2���� -   $ curator_textBox��$
��Left=�$��23��$��
35,499,0,0q�$
 Wrap=�$	��Top=�$	��420��$��1������ *	Curator+$
 TwoWay=� .  . �� -   $ label9��$+��%Форма собственности��$
��Left=�$��
546,97,0,0q�$	��Top=�$
��Bold"�$��1���� -   $ label10��$2��,Форма приема документов��$
��Left=�$��531,201,0,0q�$	��Top=�$
��Bold"�$��1���� -   $ label11��$1��+Профиль комплектования��$
��Left=�$��537,310,0,0q�$	��Top=�$
��Bold"�$��1����  �� DisplayMemberPath$ nameOwnForm�� �� SelectedValuePath$ 	idOwnForm��$
��Left=�$��537,128,0,0q�$	��Top=�$	��153��$��1������ *
OwnForms+ �� SelectedValue �� *	IdOwnForm+�� $ nameRecForm��$ 	idRecForm��$
��Left=�$��537,232,0,0q�$	��Top=�$	��153��$��1������ *
RecForms+ �� *	IdRecForm+�� $ nameColProf��$ 	idColProf��$
��Left=�$��537,341,0,0q�$	��Top=�$	��153��$��1������ *
ColProfs+ �� *	IdColProf+�� -    ��Name$ saveOrgButton��$��Сохранить��$
��Left=�$��558,514,0,0q�$	��Top=�$	��128��$��38��
����İ�$��1������ *SaveOrgCommand+�� -    ��Name$ checkBox��$
��Left=�$��27,0,0,0q�$	��Top=�$	��263��$��32��$��2�� d� 	IsChecked �� *
IsSource+.���� -    $ textBox��$��27��$
 Wrap=�$H��BЯвляется источником комплектования��$	��239��$
��Bold"�#��� ���� -!   $ item2��$'��!Архив организации��.��� 
�������.+��� -"   $ storageExistenceCheckBox��$;��5Наличие архива в организации��$
��Left=�$��	29,36,0,0q�$	��Top=� �� *StorageExistence+�� 
���   �$��1q�$
��Left=�$	��462��$��	29,76,0,0q�$	��Top=�$	��819��._�� -#   $ storageExistsPanel��$	��476��$	��Top=�$��0,0,0,-1q�.+��� $��Тип отопления��$
��Left=�$��
454,40,0,0q�$	��Top=��� $��Тип стеллажей��$
��Left=�$��
454,91,0,0q�$	��Top=� J� RenderTransformOrigin$ 1.341,-7.308&��� $��Тип шкафов��$
��Left=�$��454,145,0,0q�$	��Top=��� $)��#Описание хранилища��$
��Left=�$��
33,374,0,0q�$	��Top=��� $
��Left=�$��	33,45,0,0q�$
��Wrap=�$'��!Количество фондов��$	��Top=�$��43��$	��132���� $
��Left=�$��	33,89,0,0q�$
��Wrap=�$9��3Площадь архивохранилища (м2)��$	��Top=�$��55��$	��132���� $
��Left=�$��
33,142,0,0q�$
��Wrap=�$D��>Загруженность архивохранилища в %��$	��Top=�$��48��$	��132���� $
��Left=�$��
33,195,0,0q�$
��Wrap=�$>��8Количество штатных работников��$	��Top=�$��55��$	��132���� $
��Left=�$��
33,248,0,0q�$
��Wrap=�$Q��KСрок ведомственного хранения (кол-во лет)��$	��Top=�$��67��$	��123���� $>��8Наличие пожарной сигнализации��$
��Left=�$��
33,303,0,0q�$	��Top=� �� *	FireAlarm+�� $>��8Наличие охранной сигнализации��$
��Left=�$��
33,323,0,0q�$	��Top=� �� *SecurityAlarm+�� $[��UСоблюдение температурно-влаждностного режима��$
��Left=�$��
33,343,0,0q�$	��Top=� �� *TempAndCoolConditions+�� $
��Left=�$��23��$��
181,44,0,0q�$
 Wrap=�$	��Top=�$	��120������ *	FundCount+�� $
��Left=�$��23��$��
181,96,0,0q�$
 Wrap=�$	��Top=�$	��120��$ 0.492,0.522&����� *StorageSquare+�� $
��Left=�$��23��$��181,148,0,0q�$
 Wrap=�$	��Top=�$	��120������ *FilledCapacityPercent+�� $
��Left=�$��23��$��181,202,0,0q�$
 Wrap=�$	��Top=�$	��120������ *EmployeeCount+�� $
��Left=�$��24��$��181,254,0,0q�$
 Wrap=�$	��Top=�$	��120������ *TimeOfStorage+�� $ nameHeating��$ 	idHeating��$
��Left=�$��
551,44,0,0q�$	��Top=�$	��160������ *
Heatings+ �� *	Heating+�� $ nameShelving��$ 
idShelving��$
��Left=�$��
551,95,0,0q�$	��Top=�$	��160������ *	Shelvings+ �� *
Shelving+�� $ nameCupboard��$ 
idCupboard��$
��Left=�$��551,149,0,0q�$	��Top=�$	��160������ *	Cupboards+ �� *
Cupboard+�� $
��Left=�$��48��$��
68,400,0,0q�$
 Wrap=�$	��Top=�$	��273������ *StorageDescription+�� $
��Left=�$��551,238,0,198q�$��70�����	#FF7D838B���� *AddAboutArchiveCommand+.���� -$    ��Name$ aboutArchAdd_textBlock��$
��Wrap=�$#��Прикрепить файл��$��11(� �� TextAlignment$ Center=��� $
��Left=�$��414,248,0,0q�$
��Wrap=�$*��$Положение об архиве��$	��Top=�$��21��$	��132��$ Right=��� $
��Left=�$��414,297,0,0q�$
��Wrap=�$"��Положение об ЭК��$	��Top=�$��21��$	��132��$ Right=��� $
��Left=�$��414,345,0,0q�$
��Wrap=�$@��:Инструкция по делопроизводству��$	��Top=�$��36��$	��132��$ Right=��� $
��Left=�$��551,285,0,151q�$��70�����	#FF7D838B���� *AddExpertFileCommand+.���� -%   $ expertCommAdd_textBlock��$
��Wrap=�$#��Прикрепить файл��$ Center=�$��11(��� $
��Left=�$��551,343,0,93q�$��70�����	#FF7D838B���� *AddInstructionFileCommand+.���� -&   $ instructionAdd_textBlock��$
��Wrap=�$#��Прикрепить файл��$ Center=�$��11(��� -'   $ aboutArchDelete_Button��$
��Left=�$��714,238,0,198q�$��70�����	#FF7D838B���� *DeleteAboutArchiveCommand+.���� -(   $ 
textBlock3��$
��Wrap=�$��Удалить файл��$ Center=��� -)   $ expertCommDelete_Button��$
��Left=�$��714,285,0,151q�$��70�����	#FF7D838B���� *DeleteExpertFileCommand+.���� -*   $ 
textBlock4��$
��Wrap=�$��Удалить файл��$ Center=��� -+   $ instructionDelete_Button��$
��Left=�$��714,343,0,93q�$��70�����	#FF7D838B���� *DeleteInstructionFileCommand+.���� -,   $ 
textBlock5��$
��Wrap=�$��Удалить файл��$ Center=��� --   $ aboutArchOpen_Button��$
��Left=�$��632,238,0,198q�$��70��
�����F����� *OpenAboutArchiveCommand+.���� -.   $ 
textBlock6��$
��Wrap=�$��Открыть���� -/   $ expertCommOpen_Button��$
��Left=�$��632,285,0,151q�$��70��
�����F����� *OpenExpertFileCommand+.���� -0   $ 
textBlock7��$
��Wrap=�$��Открыть���� -1   $ instructionOpen_Button��$
��Left=�$��632,343,0,93q�$��70��
�����F����� *OpenInstructionFileCommand+.���� -2   $ 
textBlock8��$
��Wrap=�$��Открыть���� $��Сохранить��$
��Left=�$��697,543,0,0q�$	��Top=�$	��130��$��40��
����İ����� *SaveArchiveOrgCommand+�� -3   $ item3��$��Мероприятия��.��� 
�������.+�&   System.Windows.Controls.DataGrid  -4     Name$ 
eventsGrid��$
��Left=�$	��440��$��	10,10,0,0q�$	��Top=�$	��856��   AutoGenerateColumns .  �� SelectedItem �� *SelectedEvent+����  �� 
TargetType)  #System.Windows.Controls.DataGridRow"  .�-5   �� -6   $
��Left=�$��736,462,0,0q�$	��Top=�$	��130��$��40��
����İ�.���� $8��2Добавить новое мероприятие��$
��Wrap=�$ Center=��� $
��Left=�$��736,507,0,0q�$	��Top=�$	��130��$��40��
����İ����� *DeleteEventCommand+.���� $>��8Удалить выбранное мероприятие��$
��Wrap=�$ Center=��� -7   $ item4��$X��RСведения о предыдущих названиях организации��.��� 
�������.+�  -8   $ renamesGrid��$
��Left=�$	��348��$��	10,36,0,0q�$	��Top=�$	��856�� .  �� *SelectedRename+���� "  .�-9   �� -:   $
��Left=�$��697,422,0,0q�$	��Top=�$	��147��$��46��
����İ�.���� $L��FДобавить запись о предыдущем названии��$
��Wrap=�$ Center=��� $
��Left=�$��697,473,0,0q�$	��Top=�$	��147��$��46��
����İ����� *DeleteRenameCommand+.���� $6��0Удалить выделенную запись��$
��Wrap=�$ Center=��� $
��Left=�$��	10,10,0,0q�$
��Wrap=�$	��Top=�$��3��$	��798��$��14(�$
��Bold"�$��65������ *	NameOrg+