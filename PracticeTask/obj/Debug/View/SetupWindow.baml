   M S B A M L   `   `   `  ����G  CPracticeTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PracticeTask.ViewPracticeTask.View  5   	   +clr-namespace:PracticeTaskPracticeTask  5   	   #    PracticeTask.View.SetupWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PracticeTask.View     5   	   .convertclr-namespace:PracticeTask     5   	     � Title$  SetupWindow��5	   	   $	��450��5
   	   $	��300��5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��2��6   $��0��5       $1��+Кол-во активных кружков��5!      �� 5"   
   -   5        $��CountActiveCircle��5"      $��2��5#      $��1��5$      ��5%      �� *CountActiveCircle+ �� UpdateSourceTrigger$ PropertyChanged=��� 5&   
   $��2��6   $��2��5'      $ ��Кол-во кружков��5(      �� 5)   
   -   5        $��CountCircle��5)      $��2��5*      $��3��5+      ��5,      �� *CountCircle+$ PropertyChanged=��� 5-   
   $��2��6   $��4��5.      $%��Скорость кружков��5/      �� 50   
   -   5        $��Speed��50      $��2��51      $��5��52      ��53      �� *Speed+�� 54   
   $��2��6   $��6��55      $��Время теста��56      �� 57   
   -   5        $��TimeTest��57      $��2��58      $��7��59      ��5:      �� *
TimeTest+�� 5;   
   $��2��6   $��8��5<      $��Вид шариков��5=      �� 5>   
   -   5        $��
WindowView��5>      $��2��5?      $��9��5@       �� SelectedIndex 5A      �� *
WindowView+.@�5B      �� $��2D��6   �� 5C      $��3D��6   5D      �� 5E   
   $��0��6   $��10��5F      $��Сохранить��5G      ��5H      �� *SaveSetting+�� 5I   
   $��1��6   $��10��5J      $��
Назад��5K      ��5L      �� *CloseSetting+5M      5N      