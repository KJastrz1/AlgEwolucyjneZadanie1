   M S B A M L   `   `   `  ����R  NOxyPlot.Wpf, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0>.clr-namespace:OxyPlot.Wpf;assembly=OxyPlot.WpfOxyPlot.Wpf  5   	    AlgEwolucyjneZadanie1&    AlgEwolucyjneZadanie1.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   >oxy.clr-namespace:OxyPlot.Wpf;assembly=OxyPlot.Wpf     5   	     � Title$  Algorytm Genetyczny��5   	   $
��1200��6%   $
��1600��63   .��5      �� .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $
��0,10q�6K   .+�5	      �� $6��0Podaj funkcję zmiennej x np. -0.1*x^2 + 3*x + 9��6   $��0,10,5,0q�6O   �� 5
      -   5         ��Name$ FunctionInput��5
      $	��300��6-   $��30��69   $��0,0,5,0q�6E   �� 5      $��Podaj zakres np. -2,32��6   $��0,10,5,5q�66   �� 5      -   5        $ 
RangeInput��5      $��60��6*   $��30��65   $��0,0,5,0q�6A   5      �� 5   
   $ 
Horizontal=�6   $��Center=�6.   .+�5      �� $��Wielkość populacji��6   $��	10,10,5,5q�62   �� 5      -   5        $ PopulationSizeInput��5      $��60��63   $��30��6>   $��0,0,5,0q�6J   �� 5      $��Liczba iteracji��6   $��	10,10,5,5q�6/   �� 5      -   5        $ MaxGenerationsInput��5      $��60��63   $��30��6>   $��0,0,5,0q�6J   �� 5      $��Wsp. krzyżowania��6   $��	10,10,5,5q�60   �� 5      -   5        $ CrossoverRateInput��5      $��60��62   $��30��6=   $��0,0,5,0q�6I   �� 5      $��Wsp. mutacji��6   $��	10,10,5,5q�6,   �� 5      -   5        $ MutationRateInput��5      $��60��61   $��30��6<   $��0,0,5,0q�6H   5      �� 5   
   $ 
Horizontal=�6   $��Center=�6.   $
��0,10q�6K   .+�5!      �� $��Liczba powtórzeń��6   $��	20,10,5,0q�60   �� 5"      -   5        $ RepeatCountInput��5"      $��60��60   $��30��6;   $��0,0,10,0q�6G   5$      �� 5&   
   $ 
Horizontal=�6   $��Center=�6.   .+�5(      �� -   5         ��Name$ 	RunButton��5(      $��Uruchom��6(   $	��100��6:   $��30��6F   6R   5*      �� 5,   
   -	   5         ��Name$ ResultsTextBlock��5,      $
��Bold"�6.   $��	10,5,0,10q�6@   �� 5.   
   $ 
Horizontal=�6   $��Stretch=�6.   .+�5/         OxyPlot.Wpf.PlotView  -
   5          Name$ PlotFunction��5/      $	��350��61   $	��350��6=     50      -   5        $ PlotAverage��50      $	��350��60   $	��350��6<     51      -   5        $ PlotMin��51      $	��350��6,   $	��350��68     52      -   5        $ PlotMax��52      $	��350��6,   $	��350��68   53      54      55      