using AlgEwolucyjneZadanie1.Algorytm;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System.Windows;

namespace AlgEwolucyjneZadanie1;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.WindowState = WindowState.Maximized;
        SetDefaultText();
    }

    private void SetDefaultText()
    {
        FunctionInput.Text = "-0,1*x^2 + 3*x + 9";
        RangeInput.Text = "-2,32";
        PopulationSizeInput.Text = "10";
        MaxGenerationsInput.Text = "20";
        CrossoverRateInput.Text = "0,6";
        MutationRateInput.Text = "0,1";
        RepeatCountInput.Text = "1000";
    }

    private async void RunButton_Click(object sender, RoutedEventArgs e)
    {

        if (!TryGetParameters(out string function, out int start, out int end, out int populationSize, out int maxGenerations, out double crossoverRate, out double mutationRate))
            return;

        if (!int.TryParse(RepeatCountInput.Text, out int repeatCount) || repeatCount < 1 || repeatCount > 10000)
        {
            ResultsTextBlock.Text = "Nieprawidłowa liczba powtórzeń. Musi być liczbą całkowitą w zakresie 1-10 000.";
            return;
        }

        ResultsTextBlock.Text = "Ładowanie...";
        var avgFitnessSummary = new double[maxGenerations];
        var maxFitnessSummary = new double[maxGenerations];
        var minFitnessSummary = new double[maxGenerations];
        double bestOverallFitnessSum = 0;
        Individual? bestOverallIndividual = null;

        for (int i = 0; i < repeatCount; i++)
        {
            var population = await Task.Run(() =>
            {
                var pop = new Population(populationSize, mutationRate, crossoverRate, start, end, function, maxGenerations);
                pop.Run();
                return pop;
            });

            var currentBestIndividual = population.GetBestIndividual();
            bestOverallFitnessSum += currentBestIndividual.Fitness;

            if (bestOverallIndividual == null || currentBestIndividual.Fitness > bestOverallIndividual.Fitness)
            {
                bestOverallIndividual = currentBestIndividual;
            }

            for (int j = 0; j < maxGenerations; j++)
            {
                avgFitnessSummary[j] += population.AverageFitnessHistory[j];
                maxFitnessSummary[j] += population.MaxFitnessHistory[j];
                minFitnessSummary[j] += population.MinFitnessHistory[j];
            }
        }

        var avgFitness = avgFitnessSummary.Select(x => Math.Round(x / repeatCount, 2)).ToList();
        var maxFitness = maxFitnessSummary.Select(x => Math.Round(x / repeatCount, 2)).ToList();
        var minFitness = minFitnessSummary.Select(x => Math.Round(x / repeatCount, 2)).ToList();

        double averageBestFitness = Math.Round(bestOverallFitnessSum / repeatCount, 2);

        ResultsTextBlock.Text = $"Średnie wyniki dla {repeatCount} powtórzeń\n" +
                                $"Wyniki dla funkcji: {function} w zakresie [{start}, {end}]\n" +
                                $"Wielkość populacji: {populationSize}, Liczba iteracji: {maxGenerations}, " +
                                $"Wsp. krzyżowania: {crossoverRate}, Wsp. mutacji: {mutationRate}\n" +
                                $"Najlepszy osobnik: {bestOverallIndividual}, Średnie przystosowanie najlepszego osobnika: {averageBestFitness}";


        DrawPlots(function, start, end, avgFitness, maxFitness, minFitness);
    }


    private void DrawPlots(string function, int start, int end, List<double> avgFitness, List<double> maxFitness, List<double> minFitness)
    {
        DrawPlot(PlotFunction, function, start, end);
        DrawPlot(PlotAverage, avgFitness, "Średnie przystosowanie");
        DrawPlot(PlotMax, maxFitness, "Maksymalne przystosowanie");
        DrawPlot(PlotMin, minFitness, "Minimalne przystosowanie");
    }

    private void DrawPlot(PlotView plotView, string function, int start, int end)
    {
        var plotModel = new PlotModel { Title = $"Funkcja: {function}" };
        var lineSeries = new LineSeries();

        for (int x = start; x <= end; x++)
        {
            double y = FunctionProcessor.EvaluateFunction(function, x);
            lineSeries.Points.Add(new DataPoint(x, y));
        }

        plotModel.Series.Add(lineSeries);
        plotView.Model = plotModel;
    }

    private void DrawPlot(PlotView plotView, List<double> fitnessHistory, string title)
    {
        var plotModel = new PlotModel { Title = title };
        var lineSeries = new LineSeries();
        for (int i = 0; i < fitnessHistory.Count; i++)
        {
            lineSeries.Points.Add(new DataPoint(i, fitnessHistory[i]));
        }
        plotModel.Series.Add(lineSeries);
        plotView.Model = plotModel;
    }

    private bool TryGetParameters(out string function, out int start, out int end, out int populationSize, out int maxGenerations, out double crossoverRate, out double mutationRate)
    {
        populationSize = 0;
        maxGenerations = 0;
        crossoverRate = 0;
        mutationRate = 0;
        function = FunctionInput.Text.Replace(',', '.');
        if (!TryParseRange(RangeInput.Text, out start, out end))
        {
            ResultsTextBlock.Text = "Nieprawidłowy zakres. Użyj formatu: start,end.";
            return false;
        }
        if (!int.TryParse(PopulationSizeInput.Text, out populationSize) || populationSize <= 1)
        {
            ResultsTextBlock.Text = "Nieprawidłowa wielkość populacji. Musi być liczbą całkowitą większą od jeden.";
            return false;
        }
        if (!int.TryParse(MaxGenerationsInput.Text, out maxGenerations) || maxGenerations <= 0)
        {
            ResultsTextBlock.Text = "Nieprawidłowa liczba iteracji. Musi być liczbą całkowitą większą od zera.";
            return false;
        }
        if (!double.TryParse(CrossoverRateInput.Text.Replace('.', ','), out crossoverRate) || crossoverRate < 0 || crossoverRate > 1)
        {
            ResultsTextBlock.Text = "Nieprawidłowa wartość współczynnika krzyżowania. Musi być liczbą w zakresie [0, 1].";
            return false;
        }
        if (!double.TryParse(MutationRateInput.Text.Replace('.', ','), out mutationRate) || mutationRate < 0 || mutationRate > 1)
        {
            ResultsTextBlock.Text = "Nieprawidłowa wartość współczynnika mutacji. Musi być liczbą w zakresie [0, 1].";
            return false;
        }
        return true;
    }


    private bool TryParseRange(string range, out int start, out int end)
    {
        var parts = range.Split(',');
        if (parts.Length == 2 &&
            int.TryParse(parts[0].Trim(), out start) &&
            int.TryParse(parts[1].Trim(), out end))
        {
            return true;
        }
        start = end = 0;
        return false;
    }

   }
