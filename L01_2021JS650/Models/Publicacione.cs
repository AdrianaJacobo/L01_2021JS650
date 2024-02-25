using System;
using System.Collections.Generic;

namespace L01_2021JS650.Models;

public partial class Publicacione
{
    public int PublicacionId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int? UsuarioId { get; set; }
}
