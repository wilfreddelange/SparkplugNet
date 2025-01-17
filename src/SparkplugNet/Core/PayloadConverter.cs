// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PayloadConverter.cs" company="Hämmer Electronics">
// The project is licensed under the MIT license.
// </copyright>
// <summary>
//   A helper class for the payload conversions from internal ProtoBuf model to external data and vice versa.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SparkplugNet.Core;

/// <summary>
/// A helper class for the payload conversions from internal ProtoBuf model to external data and vice versa.
/// </summary>
internal static class PayloadConverter
{
    /// <summary>
    /// Gets the version A payload converted from the ProtoBuf payload.
    /// </summary>
    /// <param name="payload">The <see cref="VersionAProtoBuf.ProtoBufPayload"/>.</param>
    /// <returns>The <see cref="VersionAData.Payload"/>.</returns>
    public static VersionAData.Payload ConvertVersionAPayload(VersionAProtoBuf.ProtoBufPayload payload)
        => new VersionAData.Payload
        {
            Body = payload.Body,
            Metrics = payload.Metrics.Select(m => new VersionAData.KuraMetric
            {
                Name = m.Name,
                DataType = ConvertVersionADataType(m.Type),
                BooleanValue = m.BoolValue ?? default,
                BytesValue = m.BytesValue ?? Array.Empty<byte>(),
                DoubleValue = m.DoubleValue ?? default,
                FloatValue = m.FloatValue ?? default,
                IntValue = m.IntValue ?? default,
                LongValue = m.LongValue ?? default,
                StringValue = m.StringValue ?? string.Empty
            }).ToList(),
            Position = new VersionAData.KuraPosition
            {
                Timestamp = payload.Position?.Timestamp ?? default,
                Altitude = payload.Position?.Altitude ?? default,
                Heading = payload.Position?.Heading ?? default,
                Latitude = payload.Position?.Latitude ?? default,
                Longitude = payload.Position?.Longitude ?? default,
                Precision = payload.Position?.Precision ?? default,
                Satellites = payload.Position?.Satellites ?? default,
                Speed = payload.Position?.Speed ?? default,
                Status = payload.Position?.Status ?? default
            },
            Timestamp = payload.Timestamp
        };

    /// <summary>
    /// Gets the ProtoBuf payload converted from the version A payload.
    /// </summary>
    /// <param name="payload">The <see cref="VersionAData.Payload"/>.</param>
    /// <returns>The <see cref="VersionAProtoBuf.ProtoBufPayload"/>.</returns>
    public static VersionAProtoBuf.ProtoBufPayload ConvertVersionAPayload(VersionAData.Payload payload)
        => new VersionAProtoBuf.ProtoBufPayload
        {
            Body = payload.Body,
            Metrics = payload.Metrics.Select(m => new VersionAProtoBuf.ProtoBufPayload.KuraMetric
            {
                Type = ConvertVersionADataType(m.DataType),
                BoolValue = m.BooleanValue,
                BytesValue = m.BytesValue,
                DoubleValue = m.DoubleValue,
                FloatValue = m.FloatValue,
                IntValue = m.IntValue,
                LongValue = m.LongValue,
                Name = m.Name,
                StringValue = m.StringValue
            }).ToList(),
            Position = new VersionAProtoBuf.ProtoBufPayload.KuraPosition
            {
                Timestamp = payload.Position?.Timestamp ?? default,
                Altitude = payload.Position?.Altitude ?? default,
                Heading = payload.Position?.Heading ?? default,
                Latitude = payload.Position?.Latitude ?? default,
                Longitude = payload.Position?.Longitude ?? default,
                Precision = payload.Position?.Precision ?? default,
                Satellites = payload.Position?.Satellites ?? default,
                Speed = payload.Position?.Speed ?? default,
                Status = payload.Position?.Status ?? default
            },
            Timestamp = payload.Timestamp
        };

    /// <summary>
    /// Gets the version B payload converted from the ProtoBuf payload.
    /// </summary>
    /// <param name="payload">The <see cref="VersionBProtoBuf.ProtoBufPayload"/>.</param>
    /// <returns>The <see cref="Payload"/>.</returns>
    public static Payload ConvertVersionBPayload(VersionBProtoBuf.ProtoBufPayload payload)
        => new Payload
        {
            Body = payload.Body,
            Details = payload.Details,
            Metrics = payload.Metrics.Select(ConvertVersionBMetric).ToList(),
            Seq = payload.Seq,
            Timestamp = payload.Timestamp,
            Uuid = payload.Uuid
        };

    /// <summary>
    /// Gets the ProtoBuf payload converted from the version B payload.
    /// </summary>
    /// <param name="payload">The <see cref="Payload"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload"/>.</returns>
    public static VersionBProtoBuf.ProtoBufPayload ConvertVersionBPayload(Payload payload)
        => new VersionBProtoBuf.ProtoBufPayload
        {
            Body = payload.Body,
            Details = payload.Details,
            Metrics = payload.Metrics.Select(ConvertVersionBMetric).ToList(),
            Seq = payload.Seq,
            Timestamp = payload.Timestamp,
            Uuid = payload.Uuid
        };

    /// <summary>
    /// Gets the version A data type from the version A ProtoBuf value type.
    /// </summary>
    /// <param name="type">The <see cref="VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType"/>.</param>
    /// <returns>The <see cref="VersionAData.DataType"/>.</returns>
    public static VersionAData.DataType ConvertVersionADataType(VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType type)
        => type switch
        {
            VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Bool => VersionAData.DataType.Boolean,
            VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Bytes => VersionAData.DataType.Bytes,
            VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Double => VersionAData.DataType.Double,
            VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Float => VersionAData.DataType.Float,
            VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Int32 => VersionAData.DataType.Int32,
            VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Int64 => VersionAData.DataType.Int64,
            VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.String => VersionAData.DataType.String,
            _ => VersionAData.DataType.String
        };

    /// <summary>
    /// Gets the version A ProtoBuf value type from the version A data type.
    /// </summary>
    /// <param name="type">The <see cref="VersionAData.DataType"/>.</param>
    /// <returns>The <see cref="VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType"/>.</returns>
    public static VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType ConvertVersionADataType(VersionAData.DataType type)
        => type switch
        {
            VersionAData.DataType.Boolean => VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Bool,
            VersionAData.DataType.Bytes => VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Bytes,
            VersionAData.DataType.Double => VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Double,
            VersionAData.DataType.Float => VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Float,
            VersionAData.DataType.Int32 => VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Int32,
            VersionAData.DataType.Int64 => VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.Int64,
            VersionAData.DataType.String => VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.String,
            _ => VersionAProtoBuf.ProtoBufPayload.KuraMetric.ValueType.String
        };

    /// <summary>
    /// Gets the version B data type from the version B ProtoBuf value type for data set values.
    /// </summary>
    /// <param name="type">The <see cref="VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase"/>.</param>
    /// <returns>The <see cref="VersionBDataTypeEnum"/>.</returns>
    public static VersionBDataTypeEnum ConvertVersionBDataTypeDataSetValue(VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase type)
        => type switch
        {
            VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.None => VersionBDataTypeEnum.Unknown,
            VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.IntValue => VersionBDataTypeEnum.Int32,
            VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.LongValue => VersionBDataTypeEnum.Int64,
            VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.FloatValue => VersionBDataTypeEnum.Float,
            VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.DoubleValue => VersionBDataTypeEnum.Double,
            VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.BooleanValue => VersionBDataTypeEnum.Boolean,
            VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.StringValue => VersionBDataTypeEnum.String,
            VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.ExtensionValue => VersionBDataTypeEnum.Unknown,
            _ => VersionBDataTypeEnum.String
        };

    /// <summary>
    /// Gets the version B ProtoBuf value type from the version B data type for data set values.
    /// </summary>
    /// <param name="type">The <see cref="VersionBDataTypeEnum"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase"/>.</returns>
    public static VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase ConvertVersionBDataTypeDataSetValue(VersionBDataTypeEnum type)
        => type switch
        {
            VersionBDataTypeEnum.Unknown => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.Int8 => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int16 => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int32 => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int64 => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.UInt8 => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt16 => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt32 => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt64 => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.Float => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.FloatValue,
            VersionBDataTypeEnum.Double => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.DoubleValue,
            VersionBDataTypeEnum.Boolean => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.BooleanValue,
            VersionBDataTypeEnum.String => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.DateTime => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.Text => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.Uuid => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.DataSet => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.None,
            VersionBDataTypeEnum.Bytes => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.File => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.Template => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.None,
            VersionBDataTypeEnum.PropertySet => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.None,
            VersionBDataTypeEnum.PropertySetList => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.None,
            _ => VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.ValueOneofCase.None
        };

    /// <summary>
    /// Gets the version B data type from the version B ProtoBuf value type for metrics.
    /// </summary>
    /// <param name="type">The <see cref="VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase"/>.</param>
    /// <returns>The <see cref="VersionBDataTypeEnum"/>.</returns>
    public static VersionBDataTypeEnum ConvertVersionBDataTypeMetric(VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase type)
        => type switch
        {
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.None => VersionBDataTypeEnum.Unknown,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.IntValue => VersionBDataTypeEnum.Int32,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.LongValue => VersionBDataTypeEnum.Int64,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.FloatValue => VersionBDataTypeEnum.Float,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.DoubleValue => VersionBDataTypeEnum.Double,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.BooleanValue => VersionBDataTypeEnum.Boolean,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.StringValue => VersionBDataTypeEnum.String,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.BytesValue => VersionBDataTypeEnum.Bytes,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.DatasetValue => VersionBDataTypeEnum.DataSet,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.TemplateValue => VersionBDataTypeEnum.Template,
            VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.ExtensionValue => VersionBDataTypeEnum.Unknown,
            _ => VersionBDataTypeEnum.String
        };

    /// <summary>
    /// Gets the version B ProtoBuf value type from the version B data type for metrics.
    /// </summary>
    /// <param name="type">The <see cref="VersionBDataTypeEnum"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase"/>.</returns>
    public static VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase ConvertVersionBDataTypeMetric(VersionBDataTypeEnum type)
        => type switch
        {
            VersionBDataTypeEnum.Unknown => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.Int8 => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int16 => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int32 => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int64 => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.UInt8 => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt16 => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt32 => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt64 => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.Float => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.FloatValue,
            VersionBDataTypeEnum.Double => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.DoubleValue,
            VersionBDataTypeEnum.Boolean => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.BooleanValue,
            VersionBDataTypeEnum.String => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.DateTime => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.Text => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.Uuid => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.DataSet => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.None,
            VersionBDataTypeEnum.Bytes => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.File => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.Template => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.None,
            VersionBDataTypeEnum.PropertySet => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.None,
            VersionBDataTypeEnum.PropertySetList => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.None,
            _ => VersionBProtoBuf.ProtoBufPayload.Metric.ValueOneofCase.None
        };

    /// <summary>
    /// Gets the version B data type from the version B ProtoBuf value type for parameters.
    /// </summary>
    /// <param name="type">The <see cref="VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase"/>.</param>
    /// <returns>The <see cref="VersionBDataTypeEnum"/>.</returns>
    public static VersionBDataTypeEnum ConvertVersionBDataTypeParameter(VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase type)
        => type switch
        {
            VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.None => VersionBDataTypeEnum.Unknown,
            VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.IntValue => VersionBDataTypeEnum.Int32,
            VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.LongValue => VersionBDataTypeEnum.Int64,
            VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.FloatValue => VersionBDataTypeEnum.Float,
            VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.DoubleValue => VersionBDataTypeEnum.Double,
            VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.BooleanValue => VersionBDataTypeEnum.Boolean,
            VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.StringValue => VersionBDataTypeEnum.String,
            VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.ExtensionValue => VersionBDataTypeEnum.Unknown,
            _ => VersionBDataTypeEnum.String
        };

    /// <summary>
    /// Gets the version B ProtoBuf value type from the version B data type for parameters.
    /// </summary>
    /// <param name="type">The <see cref="VersionBDataTypeEnum"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase"/>.</returns>
    public static VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase ConvertVersionBDataTypeParameter(VersionBDataTypeEnum type)
        => type switch
        {
            VersionBDataTypeEnum.Unknown => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.Int8 => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int16 => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int32 => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int64 => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.UInt8 => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt16 => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt32 => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt64 => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.Float => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.FloatValue,
            VersionBDataTypeEnum.Double => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.DoubleValue,
            VersionBDataTypeEnum.Boolean => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.BooleanValue,
            VersionBDataTypeEnum.String => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.DateTime => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.Text => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.Uuid => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.DataSet => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.None,
            VersionBDataTypeEnum.Bytes => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.File => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.Template => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.None,
            VersionBDataTypeEnum.PropertySet => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.None,
            VersionBDataTypeEnum.PropertySetList => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.None,
            _ => VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ValueOneofCase.None
        };

    /// <summary>
    /// Gets the version B data type from the version B ProtoBuf value type for property values.
    /// </summary>
    /// <param name="type">The <see cref="VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase"/>.</param>
    /// <returns>The <see cref="VersionBDataTypeEnum"/>.</returns>
    public static VersionBDataTypeEnum ConvertVersionBDataTypePropertyValue(VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase type)
        => type switch
        {
            VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.None => VersionBDataTypeEnum.Unknown,
            VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.IntValue => VersionBDataTypeEnum.Int32,
            VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.LongValue => VersionBDataTypeEnum.Int64,
            VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.FloatValue => VersionBDataTypeEnum.Float,
            VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.DoubleValue => VersionBDataTypeEnum.Double,
            VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.BooleanValue => VersionBDataTypeEnum.Boolean,
            VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.StringValue => VersionBDataTypeEnum.String,
            VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.ExtensionValue => VersionBDataTypeEnum.Unknown,
            _ => VersionBDataTypeEnum.String
        };

    /// <summary>
    /// Gets the version B ProtoBuf value type from the version B data type for property values.
    /// </summary>
    /// <param name="type">The <see cref="VersionBDataTypeEnum"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase"/>.</returns>
    public static VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase ConvertVersionBDataTypePropertyValue(VersionBDataTypeEnum type)
        => type switch
        {
            VersionBDataTypeEnum.Unknown => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.Int8 => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int16 => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int32 => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.Int64 => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.UInt8 => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt16 => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt32 => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.IntValue,
            VersionBDataTypeEnum.UInt64 => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.Float => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.FloatValue,
            VersionBDataTypeEnum.Double => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.DoubleValue,
            VersionBDataTypeEnum.Boolean => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.BooleanValue,
            VersionBDataTypeEnum.String => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.DateTime => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.LongValue,
            VersionBDataTypeEnum.Text => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.Uuid => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.StringValue,
            VersionBDataTypeEnum.DataSet => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.None,
            VersionBDataTypeEnum.Bytes => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.File => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.ExtensionValue,
            VersionBDataTypeEnum.Template => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.None,
            VersionBDataTypeEnum.PropertySet => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.None,
            VersionBDataTypeEnum.PropertySetList => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.None,
            _ => VersionBProtoBuf.ProtoBufPayload.PropertyValue.ValueOneofCase.None
        };

    /// <summary>
    /// Gets the version B data set from the version B ProtoBuf data set.
    /// </summary>
    /// <param name="dataSet">The <see cref="VersionBProtoBuf.ProtoBufPayload.DataSet"/>.</param>
    /// <returns>The <see cref="DataSet"/>.</returns>
    private static DataSet ConvertVersionBDataSet(VersionBProtoBuf.ProtoBufPayload.DataSet dataSet)
        => new DataSet
        {
            Details = dataSet.Details,
            Columns = dataSet.Columns,
            NumOfColumns = dataSet.NumOfColumns,
            Rows = dataSet.Rows.Select(ConvertVersionBRow).ToList(),
            Types = dataSet.Types
        };

    /// <summary>
    /// Gets the version B ProtoBuf data set from the version B data set.
    /// </summary>
    /// <param name="dataSet">The <see cref="DataSet"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.DataSet"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.DataSet ConvertVersionBDataSet(DataSet dataSet)
        => new VersionBProtoBuf.ProtoBufPayload.DataSet
        {
            Details = dataSet.Details,
            Columns = dataSet.Columns,
            NumOfColumns = dataSet.NumOfColumns,
            Rows = dataSet.Rows.Select(ConvertVersionBRow).ToList(),
            Types = dataSet.Types
        };

    /// <summary>
    /// Gets the version B data set value from the version B ProtoBuf data set value.
    /// </summary>
    /// <param name="dataSetValue">The <see cref="VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue"/>.</param>
    /// <returns>The <see cref="DataSetValue"/>.</returns>
    private static DataSetValue ConvertVersionBDataSetValue(VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue dataSetValue)
        => new DataSetValue
        {
            DoubleValue = dataSetValue.DoubleValue,
            BooleanValue = dataSetValue.BooleanValue,
            ExtensionValue = new DataSetValueExtension
            {
                Details = dataSetValue.ExtensionValue.Details
            },
            FloatValue = dataSetValue.FloatValue,
            IntValue = dataSetValue.IntValue,
            LongValue = dataSetValue.LongValue,
            StringValue = dataSetValue.StringValue,
            DataType = ConvertVersionBDataTypeDataSetValue(dataSetValue.ValueCase)
        };

    /// <summary>
    /// Gets the version B ProtoBuf data set value from the version B data set value.
    /// </summary>
    /// <param name="dataSetValue">The <see cref="DataSetValue"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue ConvertVersionBDataSetValue(DataSetValue dataSetValue)
        => dataSetValue.DataType switch
        {
            VersionBDataTypeEnum.Int8
            or VersionBDataTypeEnum.Int16
            or VersionBDataTypeEnum.Int32
            or VersionBDataTypeEnum.UInt8
            or VersionBDataTypeEnum.UInt16
            or VersionBDataTypeEnum.UInt32 => new VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue
            {
                IntValue = dataSetValue.IntValue
            },
            VersionBDataTypeEnum.Int64
            or VersionBDataTypeEnum.UInt64
            or VersionBDataTypeEnum.DateTime => new VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue
            {
                LongValue = dataSetValue.LongValue
            },
            VersionBDataTypeEnum.Float => new VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue
            {
                FloatValue = dataSetValue.FloatValue
            },
            VersionBDataTypeEnum.Double => new VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue
            {
                DoubleValue = dataSetValue.DoubleValue
            },
            VersionBDataTypeEnum.Boolean => new VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue
            {
                BooleanValue = dataSetValue.BooleanValue
            },
            VersionBDataTypeEnum.String
            or VersionBDataTypeEnum.Text
            or VersionBDataTypeEnum.Uuid => new VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue
            {
                StringValue = dataSetValue.StringValue
            },
            VersionBDataTypeEnum.Unknown
            or VersionBDataTypeEnum.DataSet
            or VersionBDataTypeEnum.Bytes
            or VersionBDataTypeEnum.File
            or VersionBDataTypeEnum.Template
            or VersionBDataTypeEnum.PropertySet
            or VersionBDataTypeEnum.PropertySetList
            or _ => new VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue
            {
                ExtensionValue = new VersionBProtoBuf.ProtoBufPayload.DataSet.DataSetValue.DataSetValueExtension
                {
                    Details = dataSetValue.ExtensionValue.Details
                }
            }
        };

    /// <summary>
    /// Gets the version B row from the version B ProtoBuf row.
    /// </summary>
    /// <param name="row">The <see cref="VersionBProtoBuf.ProtoBufPayload.DataSet.Row"/>.</param>
    /// <returns>The <see cref="Row"/>.</returns>
    private static Row ConvertVersionBRow(VersionBProtoBuf.ProtoBufPayload.DataSet.Row row)
        => new Row
        {
            Details = row.Details,
            Elements = row.Elements.Select(ConvertVersionBDataSetValue).ToList()
        };

    /// <summary>
    /// Gets the version B ProtoBuf row from the version B row.
    /// </summary>
    /// <param name="row">The <see cref="Row"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.DataSet.Row"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.DataSet.Row ConvertVersionBRow(Row row)
        => new VersionBProtoBuf.ProtoBufPayload.DataSet.Row
        {
            Details = row.Details,
            Elements = row.Elements.Select(ConvertVersionBDataSetValue).ToList()
        };

    /// <summary>
    /// Gets the version B meta data from the version B ProtoBuf meta data.
    /// </summary>
    /// <param name="metaData">The <see cref="VersionBProtoBuf.ProtoBufPayload.MetaData"/>.</param>
    /// <returns>The <see cref="MetaData"/>.</returns>
    private static MetaData? ConvertVersionBMetaData(VersionBProtoBuf.ProtoBufPayload.MetaData? metaData)
        => metaData is null
        ? null
        : new MetaData
        {
            Seq = metaData.Seq,
            Details = metaData.Details,
            ContentType = metaData.ContentType,
            Description = metaData.Description,
            FileName = metaData.FileName,
            FileType = metaData.FileType,
            IsMultiPart = metaData.IsMultiPart,
            Md5 = metaData.Md5,
            Size = metaData.Size
        };

    /// <summary>
    /// Gets the version B ProtoBuf meta data from the version B meta data.
    /// </summary>
    /// <param name="metaData">The <see cref="MetaData"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.MetaData"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.MetaData? ConvertVersionBMetaData(MetaData? metaData)
        => metaData is null
        ? null
        : new VersionBProtoBuf.ProtoBufPayload.MetaData
        {
            Seq = metaData.Seq,
            Details = metaData.Details,
            ContentType = metaData.ContentType,
            Description = metaData.Description,
            FileName = metaData.FileName,
            FileType = metaData.FileType,
            IsMultiPart = metaData.IsMultiPart,
            Md5 = metaData.Md5,
            Size = metaData.Size
        };

    /// <summary>
    /// Gets the version B template from the version B ProtoBuf template.
    /// </summary>
    /// <param name="template">The <see cref="VersionBProtoBuf.ProtoBufPayload.Template"/>.</param>
    /// <returns>The <see cref="Template"/>.</returns>
    private static Template? ConvertVersionBTemplate(VersionBProtoBuf.ProtoBufPayload.Template? template)
        => template is null
        ? null
        : new Template
        {
            Metrics = template.Metrics.Select(ConvertVersionBMetric).ToList(),
            Details = template.Details,
            IsDefinition = template.IsDefinition,
            Parameters = template.Parameters.Select(ConvertVersionBParameter).ToList(),
            TemplateRef = template.TemplateRef,
            Version = template.Version
        };

    /// <summary>
    /// Gets the version B ProtoBuf template from the version B template.
    /// </summary>
    /// <param name="template">The <see cref="Template"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.Template"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.Template? ConvertVersionBTemplate(Template? template)
        => template is null
        ? null
        : new VersionBProtoBuf.ProtoBufPayload.Template
        {
            Metrics = template.Metrics.Select(ConvertVersionBMetric).ToList(),
            Details = template.Details,
            IsDefinition = template.IsDefinition,
            Parameters = template.Parameters.Select(ConvertVersionBParameter).ToList(),
            TemplateRef = template.TemplateRef,
            Version = template.Version
        };

    /// <summary>
    /// Gets the version B parameter from the version B ProtoBuf parameter.
    /// </summary>
    /// <param name="parameter">The <see cref="VersionBProtoBuf.ProtoBufPayload.Template.Parameter"/>.</param>
    /// <returns>The <see cref="Parameter"/>.</returns>
    private static Parameter ConvertVersionBParameter(VersionBProtoBuf.ProtoBufPayload.Template.Parameter parameter)
        => new Parameter
        {
            DoubleValue = parameter.DoubleValue,
            BooleanValue = parameter.BooleanValue,
            ExtensionValue = new ParameterValueExtension
            {
                Extensions = parameter.ExtensionValue.Extensions
            },
            FloatValue = parameter.FloatValue,
            IntValue = parameter.IntValue,
            LongValue = parameter.LongValue,
            Name = parameter.Name,
            StringValue = parameter.StringValue,
            ValueCase = parameter.Type,
            DataType = ConvertVersionBDataTypeParameter(parameter.ValueCase)
        };

    /// <summary>
    /// Gets the version B ProtoBuf parameter from the version B parameter.
    /// </summary>
    /// <param name="parameter">The <see cref="Parameter"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.Template.Parameter"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.Template.Parameter ConvertVersionBParameter(Parameter parameter)
    {
        VersionBProtoBuf.ProtoBufPayload.Template.Parameter pbTplParam = new()
        {
            Name = parameter.Name,
            Type = parameter.ValueCase
        };

        switch (parameter.DataType)
        {
            case VersionBDataTypeEnum.Int8:
            case VersionBDataTypeEnum.Int16:
            case VersionBDataTypeEnum.Int32:
            case VersionBDataTypeEnum.UInt8:
            case VersionBDataTypeEnum.UInt16:
            case VersionBDataTypeEnum.UInt32:
                pbTplParam.IntValue = parameter.IntValue;
                break;
            case VersionBDataTypeEnum.Int64:
            case VersionBDataTypeEnum.UInt64:
            case VersionBDataTypeEnum.DateTime:
                pbTplParam.LongValue = parameter.LongValue;
                break;
            case VersionBDataTypeEnum.Float:
                pbTplParam.FloatValue = parameter.FloatValue;
                break;
            case VersionBDataTypeEnum.Double:
                pbTplParam.DoubleValue = parameter.DoubleValue;
                break;
            case VersionBDataTypeEnum.Boolean:
                pbTplParam.BooleanValue = parameter.BooleanValue;
                break;
            case VersionBDataTypeEnum.String:
            case VersionBDataTypeEnum.Text:
            case VersionBDataTypeEnum.Uuid:
                pbTplParam.StringValue = parameter.StringValue;
                break;
            case VersionBDataTypeEnum.Unknown:
            case VersionBDataTypeEnum.DataSet:
            case VersionBDataTypeEnum.Bytes:
            case VersionBDataTypeEnum.File:
            case VersionBDataTypeEnum.Template:
            case VersionBDataTypeEnum.PropertySet:
            case VersionBDataTypeEnum.PropertySetList:
            default:
                pbTplParam.ExtensionValue = new VersionBProtoBuf.ProtoBufPayload.Template.Parameter.ParameterValueExtension
                {
                    Extensions = parameter.ExtensionValue.Extensions
                };
                pbTplParam.StringValue = parameter.StringValue;
                break;
        }

        return pbTplParam;
    }

    /// <summary>
    /// Gets the version B metric from the version B ProtoBuf metric.
    /// </summary>
    /// <param name="metric">The <see cref="VersionBProtoBuf.ProtoBufPayload.Metric"/>.</param>
    /// <returns>The <see cref="Metric"/>.</returns>
    private static Metric ConvertVersionBMetric(VersionBProtoBuf.ProtoBufPayload.Metric metric)
        => new Metric
        {
            DoubleValue = metric.DoubleValue,
            Alias = metric.Alias,
            BooleanValue = metric.BooleanValue,
            BytesValue = metric.BytesValue,
            DataSetValue = ConvertVersionBDataSet(metric.DatasetValue),
            ValueCase = metric.Datatype,
            ExtensionValue = (metric.ExtensionValue is not null) ? new MetricValueExtension
            {
                Details = metric.ExtensionValue.Details
            } : null,
            FloatValue = metric.FloatValue,
            IntValue = metric.IntValue,
            IsHistorical = metric.IsHistorical,
            IsNull = metric.IsNull,
            IsTransient = metric.IsTransient,
            LongValue = metric.LongValue,
            Metadata = ConvertVersionBMetaData(metric.Metadata),
            Name = metric.Name,
            Properties = ConvertVersionBPropertySet(metric.Properties),
            StringValue = metric.StringValue,
            Timestamp = metric.Timestamp,
            TemplateValue = ConvertVersionBTemplate(metric.TemplateValue),
            DataType = ConvertVersionBDataTypeMetric(metric.ValueCase)
        };

    /// <summary>
    /// Gets the version B ProtoBuf metric from the version B metric.
    /// </summary>
    /// <param name="metric">The <see cref="Metric"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.Metric"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.Metric ConvertVersionBMetric(Metric metric)
    {
        VersionBProtoBuf.ProtoBufPayload.Metric pbMetric = new()
        {
            Alias = metric.Alias,
            Datatype = metric.ValueCase,
            IsHistorical = metric.IsHistorical,
            IsNull = metric.IsNull,
            IsTransient = metric.IsTransient,
            Metadata = ConvertVersionBMetaData(metric.Metadata),
            Name = metric.Name,
            Properties = ConvertVersionBPropertySet(metric.Properties),
            Timestamp = metric.Timestamp
        };

        switch (metric.DataType)
        {
            case VersionBDataTypeEnum.Int8:
            case VersionBDataTypeEnum.Int16:
            case VersionBDataTypeEnum.Int32:
            case VersionBDataTypeEnum.UInt8:
            case VersionBDataTypeEnum.UInt16:
            case VersionBDataTypeEnum.UInt32:
                pbMetric.IntValue = metric.IntValue;
                break;
            case VersionBDataTypeEnum.Int64:
            case VersionBDataTypeEnum.UInt64:
            case VersionBDataTypeEnum.DateTime:
                pbMetric.LongValue = metric.LongValue;
                break;
            case VersionBDataTypeEnum.Float:
                pbMetric.FloatValue = metric.FloatValue;
                break;
            case VersionBDataTypeEnum.Double:
                pbMetric.DoubleValue = metric.DoubleValue;
                break;
            case VersionBDataTypeEnum.Boolean:
                pbMetric.BooleanValue = metric.BooleanValue;
                break;
            case VersionBDataTypeEnum.String:
            case VersionBDataTypeEnum.Text:
            case VersionBDataTypeEnum.Uuid:
                pbMetric.StringValue = metric.StringValue;
                break;
            case VersionBDataTypeEnum.DataSet:
                pbMetric.DatasetValue = ConvertVersionBDataSet(metric.DataSetValue);
                break;
            case VersionBDataTypeEnum.Template:
                pbMetric.TemplateValue = ConvertVersionBTemplate(metric.TemplateValue);
                break;
            case VersionBDataTypeEnum.Bytes:
                pbMetric.BytesValue = metric.BytesValue;
                break;
            case VersionBDataTypeEnum.Unknown:
            case VersionBDataTypeEnum.File:
            case VersionBDataTypeEnum.PropertySet:
            case VersionBDataTypeEnum.PropertySetList:
            default:
                pbMetric.ExtensionValue = (metric.ExtensionValue == null) ? null :
                    new VersionBProtoBuf.ProtoBufPayload.Metric.MetricValueExtension { Details = metric.ExtensionValue.Details };
                break;
        }

        return pbMetric;
    }

    /// <summary>
    /// Gets the version B property set list from the version B ProtoBuf property set list.
    /// </summary>
    /// <param name="propertySetList">The <see cref="VersionBProtoBuf.ProtoBufPayload.PropertySetList"/>.</param>
    /// <returns>The <see cref="PropertySetList"/>.</returns>
    private static PropertySetList? ConvertVersionBPropertySetList(VersionBProtoBuf.ProtoBufPayload.PropertySetList? propertySetList)
    {
        if (propertySetList is null)
        {
            return null;
        }

        if (propertySetList.Propertysets is null)
        {
            throw new ArgumentNullException(nameof(propertySetList), "Propertysets is not set");
        }

        return new PropertySetList
        {
            Details = propertySetList.Details,
            PropertySets = propertySetList.Propertysets.Select(ConvertVersionBPropertySet).ToNonNullList()
        };
    }

    /// <summary>
    /// Gets the version B ProtoBuf property set list from the version B property set list.
    /// </summary>
    /// <param name="propertySetList">The <see cref="PropertySetList"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.PropertySetList"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.PropertySetList? ConvertVersionBPropertySetList(PropertySetList? propertySetList)
        => propertySetList is null
        ? null
        : new VersionBProtoBuf.ProtoBufPayload.PropertySetList
        {
            Details = propertySetList.Details,
            Propertysets = propertySetList.PropertySets.Select(ConvertVersionBPropertySet).ToNonNullList()
        };

    /// <summary>
    /// Gets the version B property set from the version B ProtoBuf property set.
    /// </summary>
    /// <param name="propertySet">The <see cref="VersionBProtoBuf.ProtoBufPayload.PropertySet"/>.</param>
    /// <returns>The <see cref="PropertySet"/>.</returns>
    private static PropertySet? ConvertVersionBPropertySet(VersionBProtoBuf.ProtoBufPayload.PropertySet? propertySet)
        => propertySet is null
        ? null
        : new PropertySet
        {
            Details = propertySet.Details,
            Keys = propertySet.Keys,
            Values = propertySet.Values.Select(ConvertVersionBPropertyValue).ToList()
        };

    /// <summary>
    /// Gets the version B ProtoBuf property set from the version B property set.
    /// </summary>
    /// <param name="propertySet">The <see cref="PropertySet"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.PropertySet"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.PropertySet? ConvertVersionBPropertySet(PropertySet? propertySet)
        => propertySet is null
        ? null
        : new VersionBProtoBuf.ProtoBufPayload.PropertySet
        {
            Details = propertySet.Details,
            Keys = propertySet.Keys,
            Values = propertySet.Values.Select(ConvertVersionBPropertyValue).ToList()
        };

    /// <summary>
    /// Gets the version B property value from the version B ProtoBuf property value.
    /// </summary>
    /// <param name="propertyValue">The <see cref="VersionBProtoBuf.ProtoBufPayload.PropertyValue"/>.</param>
    /// <returns>The <see cref="PropertyValue"/>.</returns>
    private static PropertyValue ConvertVersionBPropertyValue(VersionBProtoBuf.ProtoBufPayload.PropertyValue propertyValue)
        => new PropertyValue
        {
            DoubleValue = propertyValue.DoubleValue,
            PropertySetsValue = ConvertVersionBPropertySetList(propertyValue.PropertysetsValue),
            BooleanValue = propertyValue.BooleanValue,
            ExtensionValue = new PropertyValueExtension
            {
                Details = propertyValue.ExtensionValue.Details
            },
            FloatValue = propertyValue.FloatValue,
            IntValue = propertyValue.IntValue,
            IsNull = propertyValue.IsNull,
            LongValue = propertyValue.LongValue,
            PropertySetValue = ConvertVersionBPropertySet(propertyValue.PropertysetValue),
            StringValue = propertyValue.StringValue,
            ValueCase = propertyValue.Type,
            DataType = ConvertVersionBDataTypePropertyValue(propertyValue.ValueCase)
        };

    /// <summary>
    /// Gets the version B ProtoBuf property value from the version B property value.
    /// </summary>
    /// <param name="propertyValue">The <see cref="PropertyValue"/>.</param>
    /// <returns>The <see cref="VersionBProtoBuf.ProtoBufPayload.PropertyValue"/>.</returns>
    private static VersionBProtoBuf.ProtoBufPayload.PropertyValue ConvertVersionBPropertyValue(PropertyValue propertyValue)
    {
        VersionBProtoBuf.ProtoBufPayload.PropertyValue pbPropValue = new()
        {
            IsNull = propertyValue.IsNull,
            Type = propertyValue.ValueCase
        };

        switch (propertyValue.DataType)
        {
            case VersionBDataTypeEnum.Int16:
            case VersionBDataTypeEnum.Int32:
            case VersionBDataTypeEnum.UInt8:
            case VersionBDataTypeEnum.UInt16:
            case VersionBDataTypeEnum.UInt32:
                pbPropValue.IntValue = propertyValue.IntValue;
                break;
            case VersionBDataTypeEnum.Int64:
            case VersionBDataTypeEnum.UInt64:
            case VersionBDataTypeEnum.DateTime:
                pbPropValue.LongValue = propertyValue.LongValue;
                break;
            case VersionBDataTypeEnum.Float:
                pbPropValue.FloatValue = propertyValue.FloatValue;
                break;
            case VersionBDataTypeEnum.Double:
                pbPropValue.DoubleValue = propertyValue.DoubleValue;
                break;
            case VersionBDataTypeEnum.Boolean:
                pbPropValue.BooleanValue = propertyValue.BooleanValue;
                break;
            case VersionBDataTypeEnum.String:
            case VersionBDataTypeEnum.Text:
            case VersionBDataTypeEnum.Uuid:
                pbPropValue.StringValue = propertyValue.StringValue;
                break;
            case VersionBDataTypeEnum.PropertySet:
                pbPropValue.PropertysetValue = ConvertVersionBPropertySet(propertyValue.PropertySetValue);
                break;
            case VersionBDataTypeEnum.PropertySetList:
                pbPropValue.PropertysetsValue = ConvertVersionBPropertySetList(propertyValue.PropertySetsValue);
                break;
            case VersionBDataTypeEnum.DataSet:
            case VersionBDataTypeEnum.Template:
            case VersionBDataTypeEnum.Bytes:
            case VersionBDataTypeEnum.Unknown:
            case VersionBDataTypeEnum.File:
            default:
                pbPropValue.ExtensionValue = new VersionBProtoBuf.ProtoBufPayload.PropertyValue.PropertyValueExtension
                {
                    Details = propertyValue.ExtensionValue.Details
                };
                break;
        }

        return pbPropValue;        
    }
}
