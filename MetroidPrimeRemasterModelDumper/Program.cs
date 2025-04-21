
using MetroidPrimeRemasterModelDumper;

foreach (var arg in args)
{
    if (arg.EndsWith(".pak"))
    {
        BatchPakExtractor.ExtractModels(arg);
    }
}