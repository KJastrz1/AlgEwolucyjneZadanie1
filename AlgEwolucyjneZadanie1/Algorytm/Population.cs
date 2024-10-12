namespace AlgEwolucyjneZadanie1.Algorytm;
public class Population
{
    public List<Individual> Individuals { get; private set; }
    public int Generation { get; private set; }
    public int Size { get; private set; }
    public double MutationRate { get; set; }
    public double CrossoverRate { get; set; }
    public int Start { get; private set; }
    public int End { get; private set; }
    public string Function { get; private set; }
    public int MaxGenerations { get; private set; }

    public List<double> AverageFitnessHistory { get; private set; } = new List<double>();
    public List<double> MaxFitnessHistory { get; private set; } = new List<double>();
    public List<double> MinFitnessHistory { get; private set; } = new List<double>();


    public Population(int size, double mutationRate, double crossoverRate, int start, int end, string function, int maxGenerations)
    {
        Size = size;
        MutationRate = mutationRate;
        CrossoverRate = crossoverRate;
        Start = start;
        End = end;
        Function = function;
        MaxGenerations = maxGenerations;
        Individuals = new List<Individual>();
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < Size; i++)
        {
            int gene = new Random().Next(Start, End+1);
            var individual = new Individual(gene, Function);
            Individuals.Add(individual);
        }
    }

    public void Evaluate()
    {
        foreach (var individual in Individuals)
        {
            individual.CalculateFitness(Function);
        }
    }

    public void Selection()
    {
        double alpha = 1.5;
        double totalTransformedFitness = Individuals.Sum(ind => Math.Pow(ind.Fitness, alpha));
        List<double> selectionProbabilities = new List<double>();

        foreach (var individual in Individuals)
        {
            double probability = Math.Pow(individual.Fitness, alpha) / totalTransformedFitness;
            selectionProbabilities.Add(probability);
        }

        var selectedIndividuals = new List<Individual>();
        for (int i = 0; i < Size; i++)
        {
            selectedIndividuals.Add(RouletteWheelSelect(selectionProbabilities));
        }

        Individuals = selectedIndividuals;
    }

    private Individual RouletteWheelSelect(List<double> selectionProbabilities)
    {
        double rand = new Random().NextDouble();
        double cumulativeProbability = 0.0;

        for (int i = 0; i < selectionProbabilities.Count; i++)
        {
            cumulativeProbability += selectionProbabilities[i];
            if (rand < cumulativeProbability)
            {
                return Individuals[i];
            }
        }

        return Individuals.Last();
    }

    public void Crossover()
    {
        var newIndividuals = new List<Individual>();
        for (int i = 0; i < Size / 2; i++)
        {
            var parent1 = Individuals[i * 2];
            var parent2 = Individuals[i * 2 + 1];
            if (new Random().NextDouble() < CrossoverRate)
            {
                var (child1, child2) = CrossoverPair(parent1, parent2);
                newIndividuals.Add(child1);
                newIndividuals.Add(child2);
            }
            else
            {
                newIndividuals.Add(parent1);
                newIndividuals.Add(parent2);
            }
        }
        Individuals = newIndividuals;
    }

    private (Individual, Individual) CrossoverPair(Individual parent1, Individual parent2)
    {
        double alpha = new Random().NextDouble();
        int child1Gene = (int)(alpha * parent1.Gene + (1 - alpha) * parent2.Gene);
        int child2Gene = (int)((1 - alpha) * parent1.Gene + alpha * parent2.Gene);

        var child1 = new Individual(child1Gene, Function);
        var child2 = new Individual(child2Gene, Function);
      
        return (child1, child2);
    }

    public void Mutate()
    {
        foreach (var individual in Individuals)
        {
            individual.Mutate(MutationRate, Start, End, Function);
        }
    }

    public void NextGeneration()
    {
        Selection();
        Crossover();
        Mutate();
    }

    public Individual GetBestIndividual()
    {
        return Individuals.OrderByDescending(ind => ind.Fitness).FirstOrDefault();
    }

    public void Run()
    {
        for (Generation = 0; Generation < MaxGenerations; Generation++)
        {
            NextGeneration();

            var averageFitness = Individuals.Average(ind => ind.Fitness);
            var maxFitness = Individuals.Max(ind => ind.Fitness);
            var minFitness = Individuals.Min(ind => ind.Fitness);

            AverageFitnessHistory.Add(averageFitness);
            MaxFitnessHistory.Add(maxFitness);
            MinFitnessHistory.Add(minFitness);
        }
    }
}
