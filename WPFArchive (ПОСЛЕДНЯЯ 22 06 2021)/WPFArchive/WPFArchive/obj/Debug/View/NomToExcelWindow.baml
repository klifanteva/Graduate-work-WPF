   M S B A M L   `   `   `  ����E  AWPFArchive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFArchive.ViewWPFArchive.View  5   	   ;"clr-namespace:WPFArchive.ViewModelWPFArchive.ViewModel  65   &     WPFArchive.View.NomToExcelWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WPFArchive.View     5   	   8	viewmodel"clr-namespace:WPFArchive.ViewModel     65     � Title$8  2Выгрузка номенклатуры в Excel��5   	   $	��243��6/   $��494.5��6<   .��5	      � .+�5
   
   �� -   5         ��Name$ textBox��5
      $
��Left=�6#   $��23��6>   $��
142,65,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $	��Top=�6�   $	��120��6�   ��6r   �� *	YearNom+6�   �� 5   
   -   5         ��Name$ 	textBlock��5      $
��Left=�6'   $��
117,66,0,0q�6B   $
��Wrap=�6V   $��Год��6j   $	��Top=�6u   $
��Bold"�6�    J� RenderTransformOrigin$ 2.825,0.376&�6�   �� 5   
   -   5        $ 
textBlock1��5      $
��Left=�6(   $��	-5,98,0,0q�6C   $
��Wrap=�6V   $3��-Источник комплектования��6j   $	��Top=�6�   $
��Bold"�6�   $	��142��6�    �� TextAlignment$ Right=�6�   �� 5   
   -   5         ��Name$ comboBox��5   �    �� DisplayMemberPath$ nameOrg��69    �� SelectedValuePath$ idOrg��6U   $
��Left=�6�   $��142,108,0,0q�6�   $	��Top=�6�   $	��322��6�   ��6   �� *Organizations+	 �� SelectedValue	 6o   �� *IdOrg+6�   �� 5   
   -   5        
 ��Name$
 button��5   4   $#��Создать Excel-файл��6D   $
��Left=�6a   $��350,154,0,0q�6|   $	��Top=�6�   $	��114��6�   $��38��6�   
����İ���6   �� *ToExcelCommand+6�   �� 5   
   -   5        $ 
textBlock2��5      $
��Left=�6(   $��	63,22,0,0q�6C   $
��Wrap=�6V   $]��WВведите год и выберите источник комплектования��6j   $	��Top=�6�   5      5      