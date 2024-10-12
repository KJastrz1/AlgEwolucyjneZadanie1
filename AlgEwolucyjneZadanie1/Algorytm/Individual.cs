namespace AlgEwolucyjneZadanie1.Algorytm;
public class Individual
{
    public int Gene { get; private set; }
    public double Fitness { get; private set; }

    public Individual(int gene,string function)
    {
        Gene = gene;
        CalculateFitness(function);
    }

    public void SetGene(int value, string function)
    {
        Gene = value;
        CalculateFitness(function);
    }

    public void CalculateFitness(string function)
    {
        Fitness = FunctionProcessor.EvaluateFunction(function, Gene);
    }

    public void Mutate(double mutationRate, int start, int end, string function)
    {
        if (new Random().NextDouble() < mutationRate)
        {
            Gene = new Random().Next(start, end+1);
            CalculateFitness(function);
        }
    }

    public override string ToString()
    {
        return $"x={Gene} y={Math.Round(Fitness, 2)}";
    }
}
