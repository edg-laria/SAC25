Imports HCI.Datos
Imports HCI.Entidades

Public Interface IUsuarioRepositorio
    Inherits IRepositorio(Of Usuario)
    Inherits IDisposable

    Function ObtenerPorDocumento(documento As Integer, idTipoDocumento As Integer) As Usuario

    Sub AgregarAccion(accion As Accione)


    Function ObtenerPorID(idUsuario As Integer) As Usuario


    Function AgregarRol(rol As Role) As Role

    Function ObtenerRol(idUsuario As Integer) As Role

    Overloads Function Obtener(documento As Integer, idTipoDocumento As Integer, password As String, idRolInvitado As Integer) As Boolean

    Function BuscarPersona(DNI As String, apellido As String, nombre As String, sexo As String, provincia As String, localidad As String, fechaNacimiento As String) As List(Of Persona)

    Function obtenerPersonaPorId(id As Integer) As Persona

    Function ObtenerRoles() As List(Of Role)

    Sub ActualizarRolDeUsaurio(idUsuario As Integer, idRol As Integer, idUsuarioLogueado As Integer)

    '@LL
    Sub ActualizarUnidadDeUsaurio(idPersona As Integer, idUnidad As Integer, idUsuarioLogueado As Integer)
    Function ObtenerMenuUsuario(idUsuario As Integer, menuItems As MenuItemModel())

    Function CambiarPassword(idUsuario As Integer, password As String)

    Function ObtenerUnidades() As List(Of Unidad)

    Function PopularUsuario(idPersona As Integer, idRol As Integer, password As String) As Usuario
    Function CrearPersona(personaParaGuardar As Persona, idRol As Integer, password As String) As Persona
    '@LL
    Function ActualizaPersona(personaParaGuardar As Persona, idRol As Integer, password As String) As Persona
    Function ActualizaPersona(personaParaGuardar As Persona) As Persona

    Function ObtenerPersonaPorIdPaciente(idPaciete As Integer) As Persona

    Function ObtenerPersonaPorDocumento(documento As Integer) As Persona

    Function ObtenerIdFuerzaPorDenominacion(denominacion As String) As Integer

    Function ObtenerInformacionLocalidadPorNombre(nombreLocalidad As String) As Localidad

    Function ObtenerUnidadPorId(id As Integer) As Unidad

    Function logLogin(idUsuario As Integer, IP As String) As Object


End Interface
