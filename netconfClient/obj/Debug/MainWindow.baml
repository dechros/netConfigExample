   M S B A M L   `   `   `  ����H  DnetconfClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:netconfClientnetconfClient  5   	       netconfClient.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:netconfClient     5   	   -   5          � Title$  NetConf Client��5   	   $��271.2��6    $��308.133��6/    � 
ResizeMode$ NoResize=�5	   	   .��5      � -   5        $��mainGrid��5      {�.  .+�5   
   �� $��Server Parameters:��6   $
��Left=�6-   $��	10,10,0,0q�6H   $	��Top=�6[   �� 5   
   $��Manufacturer Name:��6   $
��Left=�6-   $��	10,41,0,0q�6H   $	��Top=�6[   �� 5   
   $��Manufacturer Unique ID:��6   $
��Left=�62   $��	10,72,0,0q�6M   $	��Top=�6`   �� 5   
   $��Model Name:��6   $
��Left=�6&   $��
10,103,0,0q�6A   $	��Top=�6U   �� 5   
   $��Serial Number:��6   $
��Left=�6)   $��
10,134,0,0q�6D   $	��Top=�6X   �� 5   
   -   5         ��Name$ uniqueIdTextBox��5      $
��Left=�6+   $��23��6F   $��
156,75,0,0q�6R    �� TextWrapping$
 Wrap=�6f   $�� ��6z   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ modelNameTextBox��5      $
��Left=�6,   $��23��6G   $��156,106,0,0q�6S   $
 Wrap=�6h   $�� ��6|   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ serialTextBox��5      $
��Left=�6)   $��23��6D   $��156,137,0,0q�6P   $
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ manNameTextBox��5      $
��Left=�6*   $��23��6E   $��
156,44,0,0q�6Q   $
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $	��120��6�   �� 5   
   $��Server Status:��6   $
��Left=�6)   $��
10,165,0,0q�6D   $	��Top=�6X   �� 5   
   -   5        $ serverStatusTextBox��5       �� 
IsReadOnly . $
��Left=�6A   $��23��6\   $��156,167,0,0q�6h   $
 Wrap=�6}   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5         ��Name$ resetButton��5      $��Reset��6&   $
��Left=�66   $��121,199,0,0q�6Q   $	��Top=�6f   $��75��6~   6�   �� 5   
   -	   5        $ 
saveButton��5      $
��Save��6%   $
��Left=�64   $��201,199,0,0q�6O   $	��Top=�6d   $��75��6|   6�   5      5      