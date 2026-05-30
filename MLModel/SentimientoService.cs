using Microsoft.ML;
using Microsoft.ML.Data;

namespace Pc3_Cabana.MLModel;

public class SentimientoService
{
    private readonly PredictionEngine<SentimientoInput, SentimientoOutput> _engine;

    public SentimientoService(IWebHostEnvironment env)
    {
        var datasetPath = Path.Combine(env.ContentRootPath, "MLModel", "dataset_sentimiento.csv");
        var mlContext = new MLContext(seed: 42);

        var data = mlContext.Data.LoadFromTextFile<SentimientoInput>(
            datasetPath, hasHeader: true, separatorChar: ',');

        var pipeline = mlContext.Transforms.Conversion
            .MapValueToKey("Label", nameof(SentimientoInput.Sentimiento))
            .Append(mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimientoInput.Comentario)))
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        var model = pipeline.Fit(data);
        _engine = mlContext.Model.CreatePredictionEngine<SentimientoInput, SentimientoOutput>(model);
    }

    public string Predecir(string comentario) =>
        _engine.Predict(new SentimientoInput { Comentario = comentario }).PredictedLabel;

    public class SentimientoInput
    {
        [LoadColumn(0)] public string Comentario { get; set; } = string.Empty;
        [LoadColumn(1)] public string Sentimiento { get; set; } = string.Empty;
    }

    public class SentimientoOutput
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabel { get; set; } = string.Empty;
    }
}
