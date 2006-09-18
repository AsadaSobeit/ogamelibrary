Imports System.Threading

Public Module Ogame

    Public Enum Gid
        MetalMine = 1
        CrystalMine = 2
        DeuteriumSynthesizer = 3
        SolarPlant = 4
        FusionReactor = 12
        RoboticsFactory = 14
        NaniteFactory = 15
        Shipyard = 21
        MetalStorage = 22
        CrystalStorage = 23
        DeuteriumTank = 24
        ResearchLab = 31
        Terraformer = 33
        AllianceDepot = 34
        MissileSilo = 44
        EspionageTechnology = 106
        ComputerTechnology = 108
        WeaponsTechnology = 109
        ShieldingTechnology = 110
        ArmourTechnology = 111
        EnergyTechnology = 113
        HyperspaceTechnology = 114
        CombustionDrive = 115
        ImpulseDrive = 117
        HyperspaceDrive = 118
        LaserTechnology = 120
        IonTechnology = 121
        PlasmaTechnology = 122
        IntergalacticResearchNetwork = 123
        GravitonTechnology = 199
        SmallCargo = 202
        LargeCargo = 203
        LightFighter = 204
        HeavyFighter = 205
        Cruiser = 206
        Battleship = 207
        ColonyShip = 208
        Recycler = 209
        EspionageProbe = 210
        Bomber = 211
        SolarSatellite = 212
        Destroyer = 213
        Deathstar = 214
        RocketLauncher = 401
        LightLaser = 402
        HeavyLaser = 403
        GaussCannon = 404
        IonCannon = 405
        PlasmaTurret = 406
        SmallShieldDome = 407
        LargeShieldDome = 408
        AntiBallisticMissiles = 502
        InterplanetaryMissiles = 503
        LunarBase = 41
        SensorPhalanx = 42
        JumpGate = 43
    End Enum

    Private ReadOnly _ServerDictionary As Dictionary(Of String, Server)

    Sub New()

        _ServerDictionary = New Dictionary(Of String, Server)

    End Sub

    Public Function GetEmpire(ByVal serverName As String, ByVal username As String, ByVal password As String)

        Dim e As Empire

        Static lock As Integer = 0
        Try
            While Interlocked.CompareExchange(lock, 1, 0) <> 0
                Thread.Sleep(0)
            End While

            Dim s As Server
            If _ServerDictionary.ContainsKey(serverName) Then
                s = _ServerDictionary(serverName)
            Else
                s = New Server(serverName)
                _ServerDictionary(serverName) = s
            End If

            If s.ContainsEmpire(username) Then
                e = s.Empire(username)
            Else
                e = s.AddEmpire(username, password)
            End If
        Finally
            lock = 0
        End Try

        Return e

    End Function
End Module
