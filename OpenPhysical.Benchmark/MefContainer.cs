namespace OpenPhysical.Benchmark;

#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

#endregion

[PublicAPI]
public class MefContainer : IDisposable
{
    private readonly CompositionContainer container;

    public MefContainer()
    {
        var assemblies = new List<Assembly>
        {
            Assembly.GetExecutingAssembly(),
            Assembly.Load("OpenPhysical.CardEdge"),
            Assembly.Load("OpenPhysical.Middleware")
        };

        var catalog = new AggregateCatalog(assemblies.Select(a => new AssemblyCatalog(a)));
        this.container = new CompositionContainer(catalog);

        this.container.ComposeParts(this);
    }

    public IEnumerable<T> GetExportedValues<T>() => this.container.GetExportedValues<T>();

    public void ComposeParts(object attributedPart) => this.container.ComposeParts(attributedPart);

    /// <summary>
    /// Dispose of the MEF container
    /// </summary>
    public void Dispose()
    {
        this.container.Dispose();
        GC.SuppressFinalize(this);
    }
}
