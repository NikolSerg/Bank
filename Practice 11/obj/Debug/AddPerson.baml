   M S B A M L   `   `   `  ����F  BPractice 11, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Practice_11Practice_11  5   	       Practice_11.AddPerson   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Practice_11     5   	   -   5          �Name$  addingWindow��5   	    � Title$ 	AddPerson��5   	   $	��633��6   $	��399��6(    � Topmost .  � 
ResizeMode$ NoResize=�6C    � WindowStartupLocation$ CenterScreen=�6Y   .��5	      � .+�5
   
   �� $��Фамилия��6   $
��Left=�6"   $��	31,58,0,0q�6=   $	��Top=�6P   $��18(�6h   �� 5   
   $6��0Добавление нового клиента��6   $��Center=�64   $��0,10,0,0q�6Q   $	��Top=�6c   $��25(�6{   �� 5   
   -   5         ��Name$ lastNameBox��5      $
��Left=�6'   $��	31,97,0,0q�6B    �� TextWrapping$
 Wrap=�6U   $	��Top=�6i   $	��268��6�   $��18(�6�   6�   �� 5   
   $��Имя��6   $
��Left=�6   $��
31,141,0,0q�69   $	��Top=�6M   $��18(�6e   �� 5   
   -   5        $ firstNameBox��5      $
��Left=�6(   $��
31,180,0,0q�6C   $
 Wrap=�6W   $	��Top=�6k   $	��268��6�   $��18(�6�   6�   �� 5   
   $��Отчество��6   $
��Left=�6#   $��
31,230,0,0q�6>   $	��Top=�6R   $��18(�6j   �� 5   
   -   5        $ patroymicBox��5      $
��Left=�6(   $��
31,269,0,0q�6C   $
 Wrap=�6W   $	��Top=�6k   $	��268��6�   $��18(�6�   6�   �� 5   
   $!��Номер телефона��6   $
��Left=�6)   $��
31,322,0,0q�6D   $	��Top=�6X   $��18(�6p   �� 5   
   -   5        $ phoneBox��5      $
��Left=�6$   $��
31,361,0,0q�6?   $
 Wrap=�6S   $	��Top=�6g   $	��268��6   $��18(�6�   $��+7��6�   6�   �� 5   
   $-��'Серия, номер паспорта��6   $
��Left=�60   $��
31,414,0,0q�6K   $	��Top=�6_   $��18(�6w   �� 5   
   -   5        $ passportBox��5      $
��Left=�6'   $��
31,453,0,0q�6B   $
 Wrap=�6V   $	��Top=�6j   $	��268��6�   $��18(�6�   6�   �� 5   
   -   5         ��Name$ acceptButton��5      $��Добавить��6'   $
��Left=�6:   $��106,517,0,0q�6U   $	��Top=�6j   $	��193��6�   $��33��6�   $��18(�6�   6�   5      5      