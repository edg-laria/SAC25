Imports HCI.Datos
Imports HCI.Entidades

Public Class MockUsuarioRepositorio
    Inherits RepositorioBase(Of Usuario)
    Implements IUsuarioRepositorio
    Private usuarios As List(Of Usuario) = New List(Of Usuario)
    Private roles As List(Of Role) = New List(Of Role)

    Public Sub New(contextoDeDatos As HCIEntities)
        MyBase.New(contextoDeDatos)
    End Sub

    Public Overloads Function Obtener() As IQueryable(Of Usuario) _
        Implements IRepositorio(Of Usuario).Obtener
        Return Me.usuarios.AsQueryable()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Overloads Function Insertar(entidad As Usuario) As Usuario _
        Implements IRepositorio(Of Usuario).Insertar
        usuarios.Add(entidad)
        entidad.idUsuario = usuarios.Count() + 1
        Return entidad
    End Function

    Public Function ObtenerPorDocumento(documento As Integer, idTipoDocumento As Integer) As Usuario _
        Implements IUsuarioRepositorio.ObtenerPorDocumento

        Dim accion As Accione = New Accione With {.controlador = "home", .idSistema = 1, .nombre = "index"}
        Dim rol As Role = New Role With {.AccionesPorRoles = New List(Of AccionesPorRole)}

        Dim accionPorRol = New AccionesPorRole With {.Accione = accion, .Role = rol}
        rol.AccionesPorRoles.Add(accionPorRol)

        Dim usuario As Usuario = New Usuario With {.idRol = 1}
        Return usuario

    End Function

    Public Sub AgregarAccion(accion As Accione) Implements IUsuarioRepositorio.AgregarAccion

    End Sub


    Public Function AgregarRol(rol As Role) As Role Implements IUsuarioRepositorio.AgregarRol
        roles.Add(rol)
        Return rol
    End Function

    Public Function ObtenerRol(idRol As Integer) As Role Implements IUsuarioRepositorio.ObtenerRol
        Return roles.Where(Function(rol) rol.idRol = idRol).FirstOrDefault()
    End Function

    Public Overloads  Function Obtener(documento As Integer, idTipoDocumento As Integer, password As String, idRolInvitado As Integer) As Boolean Implements IUsuarioRepositorio.Obtener
        Return usuarios.Exists(Function(usuario) password = usuario.password _
                                   And usuario.Persona.documento = documento _
                                   And usuario.Persona.idTipoDocumento = idTipoDocumento)
    End Function

    Public Function BuscarPersona(DNI As String, apellido As String, nombre As String, sexo As String, provincia As String, localidad As String, fechaNacimiento As String) As List(Of Persona) Implements IUsuarioRepositorio.BuscarPersona
        Throw New NotImplementedException()
    End Function

    Public Function obtenerPersonaPorId(id As Integer) As Persona Implements IUsuarioRepositorio.obtenerPersonaPorId
    End Function

    Public Function ObtenerPorID(idUsuario As Integer) As Usuario Implements IUsuarioRepositorio.ObtenerPorID
    End Function


    Public Function ObtenerRoles() As List(Of Role) Implements IUsuarioRepositorio.ObtenerRoles
        Throw New NotImplementedException()
    End Function

    Public Function ObtenerMenuUsuario(idUsuario As Integer, menuItems() As MenuItemModel) As Object Implements IUsuarioRepositorio.ObtenerMenuUsuario
        Throw New NotImplementedException()
    End Function

    Public Function CambiarPassword(idUsuario As Integer, password As String) Implements IUsuarioRepositorio.CambiarPassword
    End Function

    Public Function ObtenerUnidades() As List(Of Unidad) Implements IUsuarioRepositorio.ObtenerUnidades
    End Function


    Public Function ObtenerPersonaPorIdPaciente(idPaciete As Integer) As Persona Implements IUsuarioRepositorio.ObtenerPersonaPorIdPaciente

    End Function

    Function ObtenerPersonaPorDocumento(documento As Integer) As Persona Implements IUsuarioRepositorio.ObtenerPersonaPorDocumento

    End Function

    Public Function ObtenerIdFuerzaPorDenominacion(denominacion As String) As Integer Implements IUsuarioRepositorio.ObtenerIdFuerzaPorDenominacion
        Return 1
    End Function

    Public Function ObtenerInformacionLocalidadPorNombre(nombreLocalidad As String) As Localidad Implements IUsuarioRepositorio.ObtenerInformacionLocalidadPorNombre
    End Function

    Public Sub ActualizarRolDeUsaurio(idUsuario As Integer, idRol As Integer, idUsuarioLogueado As Integer) Implements IUsuarioRepositorio.ActualizarRolDeUsaurio
        Throw New NotImplementedException()
    End Sub
    '@LL
    Public Function ActualizaPersona(personaParaGuardar As Persona, idRol As Integer, password As String) As Persona Implements IUsuarioRepositorio.ActualizaPersona
        Throw New NotImplementedException()
    End Function
    Public Function ActualizaPersona(personaParaGuardar As Persona) As Persona Implements IUsuarioRepositorio.ActualizaPersona
        Throw New NotImplementedException()
    End Function
    Public Function ObtenerUnidadPorId(id As Integer) As Unidad Implements IUsuarioRepositorio.ObtenerUnidadPorId
        Throw New NotImplementedException()
    End Function

    Public Sub ActualizarUnidadDeUsaurio(idPersona As Integer, idUnidad As Integer, idUsuarioLogueado As Integer) Implements IUsuarioRepositorio.ActualizarUnidadDeUsaurio
        Throw New NotImplementedException()
    End Sub

    Public Function PopularUsuario(idPersona As Integer, idRol As Integer, password As String) As Usuario Implements IUsuarioRepositorio.PopularUsuario
        Throw New NotImplementedException()
    End Function

    Public Function CrearPersona(personaParaGuardar As Persona, idRol As Integer, password As String) As Persona Implements IUsuarioRepositorio.CrearPersona
        Throw New NotImplementedException()
    End Function

    Public Function ObtenerInforme(idUsuario As Integer, ip As String) As Object Implements IUsuarioRepositorio.logLogin
        Throw New NotImplementedException()
    End Function

End Class
