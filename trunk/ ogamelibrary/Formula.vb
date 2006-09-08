Imports Microsoft.VisualBasic

Public Module Formula

#Region "Interplanetary Rockets"

    'Interplanetary Rockets
    'Range(impulse engine level)*2-1=systems the rocket can fly
    'Speed= 30 seconds in your system
    'and for every system you have to fly over add 60 seconds.
    'cannot send these rockets over galaxies

    'Systems the rocket can fly
    Public Function InterplanetaryRocketRange(ByVal ImpulseEngineLevel As Integer) As Integer

        Return ImpulseEngineLevel * 2 - 1

    End Function

    'Speed
    Public Function RocketFlightInterval(ByVal Distance As Integer) As Integer

        RocketFlightInterval = 30 + 60 * Distance
    End Function
#End Region

#Region "Energy output per solar satellite of that planet = (Max temp/4)+20."

    Public Function SatellitePowerGeneration(ByVal MaxTemperature As Integer) As Integer

        SatellitePowerGeneration = MaxTemperature / 4 + 20
    End Function
#End Region

#Region "production per hour"

    'production per hour:
    'metalmine:
    'production = 30 * level * 1,1^ level
    'crystalmine:
    'production = 20 * level * 1,1^ level
    'deutsy.:
    'production = 10 * level *1,1^level*(-0,002*max-temperature+1,28 )
    'solarplant:
    'production = 20 * level * 1,1^ level
    'fusionplant:
    'production = 50 * level *1,1^ level

    ''' <summary>
    ''' metal mine production rate
    ''' </summary>
    ''' <param name="Level">metal mine level</param>
    ''' <returns>production per hour</returns>
    ''' <remarks></remarks>
    Public Function MetalProduction(ByVal Level As Integer) As Integer

        Return 30 * Level * 1.1 ^ Level

    End Function

    ''' <summary>
    ''' crystal mine production rate
    ''' </summary>
    ''' <param name="Level">crystal mine level</param>
    ''' <returns>production per hour</returns>
    ''' <remarks></remarks>
    Public Function CrystalProduction(ByVal Level As Integer) As Integer

        Return 20 * Level * 1.1 ^ Level

    End Function

    ''' <summary>
    ''' deuterium synthersizer production rate
    ''' </summary>
    ''' <param name="Level">deuterium synthesizer level</param>
    ''' <param name="HighestTemperature">highest temperature of the planet</param>
    ''' <returns>production per hour</returns>
    ''' <remarks></remarks>
    Public Function DeuteriumProduction(ByVal Level As Integer, ByVal HighestTemperature As Integer) As Integer

        Return 10 * Level * 1.1 ^ Level * (-0.002 * HighestTemperature + 1.28)

    End Function

    ''' <summary>
    ''' solar planet production rate
    ''' </summary>
    ''' <param name="Level">solar planet level</param>
    ''' <returns>production per hour</returns>
    ''' <remarks></remarks>
    Public Function SolarplantProduction(ByVal Level As Integer) As Integer

        Return 20 * Level * 1.1 ^ Level

    End Function

    ''' <summary>
    ''' fusion planet production rate
    ''' </summary>
    ''' <param name="Level">fusion planet level</param>
    ''' <returns>production per hour</returns>
    ''' <remarks></remarks>
    Public Function FusionplantProduction(ByVal Level As Integer) As Integer

        Return 50 * Level * 1.1 ^ Level

    End Function
#End Region

#Region "consumption per hour"

    'consumption per hour:
    'energyconsumption of metalmine is:
    '= 10 * level*1,1^ level
    'energyconsumption of crystalmine is:
    '= 10 * level*1,1^level
    'energyconsumption of deutsy. is:
    '= 20 * level*1,1^ level
    'deutconsumption of fusionplant is:
    '= 10 * level*1,1^ level*(-0,002*max-temperature+1,28 )

    Public Function MetalmineEnergyConsumption(ByVal Level As Integer) As Integer

        MetalmineEnergyConsumption = 10 * Level * 1.1 ^ Level
    End Function

    Public Function CrystalmineEnergyConsumption(ByVal Level As Integer) As Integer

        CrystalmineEnergyConsumption = 10 * Level * 1.1 ^ Level
    End Function

    Public Function DeutmineEnergyConsumption(ByVal Level As Integer) As Integer

        DeutmineEnergyConsumption = 20 * Level * 1.1 ^ Level
    End Function

    Public Function DeutConsumption(ByVal Level As Integer, ByVal MaxTemperature As Integer) As Integer

        DeutConsumption = 10 * Level * 1.1 ^ (-0.002 * MaxTemperature + 1.28)
    End Function
#End Region

    'calculation of the buildingsbuildtimes:
    '[(crys+met) / 2500] * [1 / (level robofactory+1)] * 0,5^level nani
    Public Function BuildingTime() As Single

    End Function
End Module
