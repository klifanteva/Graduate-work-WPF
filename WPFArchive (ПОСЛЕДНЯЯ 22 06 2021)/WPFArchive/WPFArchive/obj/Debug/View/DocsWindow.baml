   M S B A M L   `   `   `  ����E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  5   	   ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  65        WPFArchive.View.DocsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WPFArchive.View     5   	   8	viewmodel"clr-namespace:WPFArchive.ViewModel     65   -   5          � Title$  
DocsWindow��5	   	   $��556.3��6   $��528.5��6+   .��5
      � .+�5   
   �� $6��0Добавить документ в опись:��6   $
��Left=�65   $��	10,10,0,0q�6P   $	��Top=�6c   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��35��6T   $��	10,41,0,0q�6`   $	��Top=�6s   $	��500��6�   ._�5      �� $
��Left=�6   $
��Wrap=�63   $��
-1,-1,-1,0q�6b   $	��500��6v   $��34��6�   $	��Top=�6�   ��6G   �� *	SetOfDocs+6�   5      �� 5   
   $#��Номер документа��6   $
��Left=�6*   $��
26,136,0,0q�6E   $	��Top=�6Y   $
��Bold"�6q   �� 5   
   $'��!Дата формирования��6   $
��Left=�6,   $��
12,240,0,0q�6G   $	��Top=�6[   $
��Bold"�6s   �� 5   
   $��Тип документа��6   $
��Left=�6(   $��
44,282,0,0q�6C   $	��Top=�6W   $
��Bold"�6o   �� 5   
   $
��Left=�6   $��23��6-   $��140,139,0,0q�69    �� TextWrapping$
 Wrap=�6N   $	��Top=�6}   $	��120��6�   ��6b   �� *	NumberDoc+6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ date_datePicker��5      $
��Left=�6O   $��140,242,0,0q�6j   $	��Top=�6   $	��155��6�      SelectedDate 6.   �� *	DateDoc+6�   �� 5   
   $
��Left=�6   $��140,286,0,0q�6.   $	��Top=�6C   $	��155��6[    �� DisplayMemberPath$ nameDocType��6�    �� SelectedValuePath$ 	idDocType��6�   ��6g   �� *
DocTypes+ �� SelectedValue 6�   �� *	DocType+�� 5   
   $
��Left=�65   $��140,373,0,0q�6P   $	��Top=�6e   $	��131��6}   $��42��6�   
�����F���6   �� *SaveFileCommand+.��5      �� -   5         ��Name$ saveFileButton��5      $
��Wrap=�60   $.��(Прикрепить новый файл��6D    �� TextAlignment$ Center=�6a   5      �� 5   
   $��Сохранить��61   $
��Left=�6E   $��354,464,0,0q�6`   $	��Top=�6u   $	��122��6�   $��42��6�   
����İ���6   �� *SaveCommand+6�   �� 5   
   $��Номенклатура��6   $
��Left=�6'   $��
35,196,0,0q�6B   $	��Top=�6V   $
��Bold"�6n   �� 5   
   $ idNom��69   $
��Left=�6z   $��140,200,0,0q�6�   $	��Top=�6�   $	��350��6�   ��6   �� *Nomenclatures+ 6S   �� *Nomenclature+��5      �� 5      .R�5      �� ��5      `� 5       	 �� StringFormat$	 {0} {1}��6+   .1�5!   "   �� 
 �� Path$
 actIndex�6*   �� 5"   "   $
 
actHeading�6*   5#      5%      5&      5(      �� 5)   
   -   5        $ 	textBlock��5)      $
��Left=�6'   $��140,187,-37,0q�6B   $
��Wrap=�6Y   $���}номенклатура выведена в соотвествии с ИК и годом, указанными в описи��6m   $	��Top=�6�   $	��417��6�   $��10(�6�   �� 5*   
   -   5         ��Name$ textBox��5*      $
��Left=�6#   $��23��6>   $��140,330,0,0q�6J   $
 Wrap=�6_   $	��Top=�6�   $	��120��6�   ��6s   �� *SheetsCount+6�   �� 5+   
   -   5        $ 
textBlock1��5+      $
��Left=�6(   $��
64,321,0,0q�6C   $
��Wrap=�6W   $'��!Количество листов��6k   $	��Top=�6�   $��78��6�   $
��Bold"�6�   �� 5,   
   -   5        $ fileExsists��5,      $
��Left=�6)   $��283,372,0,0q�6D   $
��Wrap=�6Y   $W��Qдля этого документа еще не был добавлен файл��6m   $	��Top=�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   $	��124��6�   $��10(�6�   $��43��6�   $ Center=�6�   z�5-      V� 5.      .~�5/      �� �� 50       �� AngleX$ -0.001Q�6$   �� 51       �� Angle$ -0.009Q�6&   T� 52      53      55      �� 56   
   -   5         ��Name$ deleteFileButton��56   7   $��Удалить файл��6Q   
�����F�$
��Left=�6   $��140,431,0,0q�6�   $	��Top=�6�   $	��131��6�   $��36��6�   $y�Hidden=�6�   ��6   �� *DeleteFileCommand+6�   57      58      