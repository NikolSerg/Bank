   M S B A M L   `   `   `  ����C  ?Bank__v1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Bank__v1Bank__v1  5   	       Bank__v1.DataBase   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Bank__v1     5   	     � Title$  DataBase��5   	   $	��450��6   $	��800��6'    � WindowStartupLocation$ CenterScreen=�63   $	��660��6X   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ ClientsDataGrid��5
         AutoGenerateColumns .  $��Center=�6H   $��0,0,0,76q�6e      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Фамилия��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   ��  �� Path$ LastName�6X     5      $ Имя��6%   $ 1* 6U    62   �� $ 	FirstName�6U     5      $ Отчество��6%   $ 1* 6[    67   �� $ 
Patronymic�6[     5      $! Номер телефона��6%   $ 1* 6b    6=   �� $ PhoneNumber�6b   4  .System.Windows.Controls.DataGridTemplateColumn  5      $- 'Серия, номер паспорта��6)   $ 1* 6H   	   CellTemplate	 5      �� 5      .R�5      �� $��**** ******��6(   5      
   CellEditingTemplate
 5      �� 5      .R�5      �� ��6&   �� $ Passport�5      5      5      �� 5   
   -   5         ��Name$ 	addButton��5      $1��+Добавить клиента в базу��6$   $
��Left=�6F   $��	10,0,0,10q�6a   $��55��6t   $��Bottom=�6�   $	��181��6�   {�.  6�   �� 5   
   -   5        $ removeButton��5      $1��+Удалить клиента из базы��6'   $
��Left=�6I   $��
219,0,0,10q�6d   $��55��6x   $��Bottom=�6�   $	��181��6�   {�.  6�   �� 5    
   -   5        $ showHistoryButton��5       $<��6Посмотреть историю изменений��6,   $
��Left=�6S   $��
428,0,0,10q�6n   $	��190��6�   {�.  $��55��6�   $��Bottom=�6�   5"      5#      