   M S B A M L   `   `   `  ����G  CPracticeTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PracticeTask.ViewPracticeTask.View  5   	   +clr-namespace:PracticeTaskPracticeTask  5   	   $    PracticeTask.View.TestWindow2D   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PracticeTask.View     5   	   1
convHeightclr-namespace:PracticeTask     5   	     � Title$  TestWindow2D��5	   	   $	��450��5
   	   $	��800��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�5   	    � 	Resources 5          PracticeTask.ConverterSize   multivalueConverter   BooleanToVisibilityConverter��%:   &	        &	        5   
   6#   �� 5   
   6'   5      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $	��10* �6   �� 5      $��* �6   .+�5   
   �� -   5        $��ItemsControl��5      $��2��5      $��0��5      ��5      �� *	Circles+y�5      �� *IsShowItemsControl+ �� 	Converter# [ ��5       �� 5!      .R�5"      ��  ��Name$ Canvas��6   .+�5#      A� -   5        ��5$      `� 5%   "    `� 	Converter# [  6/   .1�5&   &   �� 	 �� Path$	 
SizeCircle�6.   �� 5'   &   
 �� ElementName$
 ItemsControl��6.   $	 ActualWidth�6I   5(   #   ��5*      `� 5+   "   # [  6/   .1�5,   &   �� $	 
SizeCircle�6.   �� 5-   &   $
 ItemsControl��6.   $	 ActualWidth�6I   5.   #    �� Left 50      `� 51   "   # [  6/   .1�52   &   �� $	 X�6.   �� 53   &   $
 ItemsControl��6.   $	 ActualWidth�6I   54   #   
 �� Top 56      `� 57   "   # [  6/   .1�58   &   �� $	 Y�6.   �� 59   &   $
 ItemsControl��6.   $	 ActualHeight�6I   5:   #    5<       �� 
TargetType �� Property �� Value �� Triggers �� Binding �� Value��%�   'A�       �� 5=   "   " A�6(   .�5>   &   ��  � ��
 �  ��6=    5?   &   �� 5@   *    66   �� *IsActiveColor+ true6X   .a�5A   .   ��  � ��$ 2��6P   �� 5B   .    � ��
 �   �6G   5C   +   5E   #   5F      5G      5H      5I      5K      �� 5L   
   $��0��6   $��2��5M      $��Center=�5O      $��Center=�5P      $��40(�5Q      ��5N      �� *TextBlockText+y�5R      �� *IsShowTextBlock+# [ 5T      �� 5U   
   $��0��6   $��1��5V      $��Начать��5W      y�5X      �� *IsShowButtonStart+# [ ��5Y      �� *Start+�� 5Z   
   $��0��6   $��1��5[      $��Проверить��5\      y�5]      �� *IsShowButtonCheck+# [ ��5^      �� *Check+�� 5_   
   $��1��6   $��2��5`      $��Закрыть��5a      ��5b      �� *	CloseTest+5c      5d      