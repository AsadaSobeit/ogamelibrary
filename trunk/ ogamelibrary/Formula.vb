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

        Return 30 + 60 * Distance
    End Function
#End Region

#Region "Energy output per solar satellite of that planet = (Max temp/4)+20."

    Public Function SatellitePowerGeneration(ByVal MaxTemperature As Integer) As Integer

        Return MaxTemperature / 4 + 20
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

    ''' <summary>
    ''' calculate how many hours does it cost to construct a building
    ''' </summary>
    ''' <param name="metal">metal required</param>
    ''' <param name="crystal">crystal required</param>
    ''' <param name="roboticsFactoryLevel">level of robotics factory</param>
    ''' <param name="naniteFactoryLevel">level of nanite factory</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BuildingsHour(ByVal metal As Integer, ByVal crystal As Integer, ByVal roboticsFactoryLevel As Integer, ByVal naniteFactoryLevel As Integer) As Double

        Return (metal + crystal) / 2500 / (roboticsFactoryLevel + 1) * 0.5 ^ naniteFactoryLevel

    End Function

    ''' <summary>
    ''' calculate how many hours does it cost to build a ship or battery
    ''' </summary>
    ''' <param name="metal">metal required</param>
    ''' <param name="crystal">crystal required</param>
    ''' <param name="shipyardLevel">level of shipyard</param>
    ''' <param name="naniteFactoryLevel">level of nanite factory</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ShipyardHour(ByVal metal As Integer, ByVal crystal As Integer, ByVal shipyardLevel As Integer, ByVal naniteFactoryLevel As Integer) As Double

        Return (metal + crystal) / 2500 / (shipyardLevel + 1) * 0.5 ^ naniteFactoryLevel

    End Function

    ''' <summary>
    ''' calculate how many hours does it cost to complete a research
    ''' </summary>
    ''' <param name="metal">metal required</param>
    ''' <param name="crystal">crystal required</param>
    ''' <param name="labLevel">level of research laboratory</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResearchLabHour(ByVal metal As Integer, ByVal crystal As Integer, ByVal labLevel As Integer) As Double

        Return (metal + crystal) / (1 + labLevel) / 1000

    End Function

    Public Function MetalMineMetalCost(ByVal level As Integer) As Integer

        Return 60 * 1.5 ^ (level - 1)

    End Function

    Public Function MetalMineCrystalCost(ByVal level As Integer) As Integer

        Return 15 * 1.5 ^ (level - 1)

    End Function

    Public Function CrystalMineMetalCost(ByVal level As Integer) As Integer

        Return 48 * 1.6 ^ (level - 1)

    End Function

    Public Function CrystalMineCrystalCost(ByVal level As Integer) As Integer

        Return 24 * 1.6 ^ (level - 1)

    End Function

    Public Function DeuteriumSynthesizerMetalCost(ByVal level As Integer) As Integer

        Return 225 * 1.5 ^ (level - 1)

    End Function

    Public Function DeuteriumSynthesizerCrystalCost(ByVal level As Integer) As Integer

        Return 75 * 1.5 ^ (level - 1)

    End Function

    Public Function SolarPlantMetalCost(ByVal level As Integer) As Integer

        Return 75 * 1.5 ^ (level - 1)

    End Function

    Public Function SolarPlantCrystalCost(ByVal level As Integer) As Integer

        Return 30 * 1.5 ^ (level - 1)

    End Function

    Public Function FusionPlantMetalCost(ByVal level As Integer) As Integer

        Return 900 * 1.8 ^ (level - 1)

    End Function

    Public Function FusionPlantCrystalCost(ByVal level As Integer) As Integer

        Return 360 * 1.8 ^ (level - 1)

    End Function

    Public Function FusionPlantDeuteriumCost(ByVal level As Integer) As Integer

        Return 180 * 1.8 ^ (level - 1)

    End Function
End Module
