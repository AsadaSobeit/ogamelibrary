Imports System.ComponentModel
Imports System.Math

<DataObject(True)> _
Public Class PlanetView

#Region "variables"

    Private ReadOnly _Planet As Planet

    Private _LocalTime As Date
    Private _DeuteriumDepletedETA As Date

    'begin: before deuterium depleted

    Private _MetalProduction_0 As Double
    Private _CrystalProduction_0 As Double
    Private _DeuteriumProduction_0 As Double
    Private _DeuteriumConsumption_0 As Double

    Private _MetalMineEnergyConsumption_0 As Double
    Private _CrystalMineEnergyConsumption_0 As Double
    Private _DeuteriumSynthesizerEnergyConsumption_0 As Double
    Private _EnergyConsumptionTotal_0 As Double

    Private _FusionReactorEnergyGeneration_0 As Double
    Private _EnergyGenerationPotentialTotal_0 As Double

    'end: before deuterium depleted

    'begin: on deuterium depleted

    Private _Metal_D As Double
    Private _Crystal_D As Double
    Private _Deuterium_D As Double

    'end: on deuterium depleted

    'begin: after deuterium depleted

    Private _MetalProduction_1 As Double
    Private _CrystalProduction_1 As Double
    Private _DeuteriumProduction_1 As Double
    Private _DeuteriumConsumption_1 As Double

    Private _MetalMineEnergyConsumption_1 As Double
    Private _CrystalMineEnergyConsumption_1 As Double
    Private _DeuteriumSynthesizerEnergyConsumption_1 As Double
    Private _EnergyConsumptionTotal_1 As Double

    Private _FusionReactorEnergyGeneration_1 As Double
    Private _EnergyGenerationPotentialTotal_1 As Double

    'end: after deuterium depleted

    Private _MetalProductionPotential As Double
    Private _CrystalProductionPotential As Double
    Private _DeuteriumProductionPotential As Double
    Private _DeuteriumConsumptionPotential As Double

    'Private _EnergyGenerationPotentialTotal As Double
    Private _EnergyConsumptionPotentialTotal As Double

    Private _MetalMineEnergyConsumptionPotential As Double
    Private _CrystalMineEnergyConsumptionPotential As Double
    Private _DeuteriumSynthesizerEnergyConsumptionPotential As Double

    Private _FusionReactorEnergyGenerationPotential As Double

    Private _SolarPlanetEnergyGeneration As Double
    'Private _FusionReactorEnergyGeneration As Double
    Private _SolarSateliteEnergyGeneration As Double

    Private _MetalMineProductionPercentage As Integer
    Private _CrystalMineProductionPercentage As Integer
    Private _DeuteriumSynthesizerProductionPercentage As Integer
    Private _SolarPlanetProductionPercentage As Integer
    Private _FusionReactorProductionPercentage As Integer
    Private _SolarSateliteProductionPercentage As Integer

    Private _MetalMineLevel As Integer
    Private _CrystalMineLevel As Integer
    Private _DeuteriumSynthesizerLevel As Integer
    Private _SolarPlanetLevel As Integer
    Private _FusionReactorLevel As Integer
    Private _SolarSateliteLevel As Integer

    Private _MetalStorageLevel As Integer
    Private _CrystalStorageLevel As Integer
    Private _DeuteriumTankLevel As Integer

    Private _MetalCapacity As Double
    Private _CrystalCapacity As Double
    Private _DeuteriumCapacity As Double

    Private _MetalOverflowETA As Date
    Private _CrystalOverflowETA As Date
    Private _DeuteriumOverflowETA As Date

#End Region

    ''' <summary>
    ''' todo: set Double.NaN
    ''' </summary>
    ''' <param name="p"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal p As Planet)

        _Planet = p
        AddHandler p.Changed, AddressOf ChangedEventHandler

        'begin: before deuterium depleted

        _MetalProduction_0 = Double.NaN
        _CrystalProduction_0 = Double.NaN
        _DeuteriumProduction_0 = Double.NaN
        _DeuteriumConsumption_0 = Double.NaN

        _MetalMineEnergyConsumption_0 = Double.NaN
        _CrystalMineEnergyConsumption_0 = Double.NaN
        _DeuteriumSynthesizerEnergyConsumption_0 = Double.NaN
        _EnergyConsumptionTotal_0 = Double.NaN

        _FusionReactorEnergyGeneration_0 = Double.NaN
        _EnergyGenerationPotentialTotal_0 = Double.NaN

        'end: before deuterium depleted

        'begin: on deuterium depleted

        _Metal_D = Double.NaN
        _Crystal_D = Double.NaN
        _Deuterium_D = Double.NaN

        'end: on deuterium depleted

        'begin: after deuterium depleted

        _MetalProduction_1 = Double.NaN
        _CrystalProduction_1 = Double.NaN
        _DeuteriumProduction_1 = Double.NaN
        _DeuteriumConsumption_1 = Double.NaN

        _MetalMineEnergyConsumption_1 = Double.NaN
        _CrystalMineEnergyConsumption_1 = Double.NaN
        _DeuteriumSynthesizerEnergyConsumption_1 = Double.NaN
        _EnergyConsumptionTotal_1 = Double.NaN

        _FusionReactorEnergyGeneration_1 = Double.NaN
        _EnergyGenerationPotentialTotal_1 = Double.NaN

        'end: after deuterium depleted

        _MetalProductionPotential = Double.NaN
        _CrystalProductionPotential = Double.NaN
        _DeuteriumProductionPotential = Double.NaN
        _DeuteriumConsumptionPotential = Double.NaN

        _EnergyConsumptionPotentialTotal = Double.NaN

        _MetalMineEnergyConsumptionPotential = Double.NaN
        _CrystalMineEnergyConsumptionPotential = Double.NaN
        _DeuteriumSynthesizerEnergyConsumptionPotential = Double.NaN

        _FusionReactorEnergyGenerationPotential = Double.NaN

        _SolarPlanetEnergyGeneration = Double.NaN
        _SolarSateliteEnergyGeneration = Double.NaN

        _MetalMineProductionPercentage = -1
        _CrystalMineProductionPercentage = -1
        _DeuteriumSynthesizerProductionPercentage = -1
        _SolarPlanetProductionPercentage = -1
        _FusionReactorProductionPercentage = -1
        _SolarSateliteProductionPercentage = -1

        _MetalMineLevel = -1
        _CrystalMineLevel = -1
        _DeuteriumSynthesizerLevel = -1
        _SolarPlanetLevel = -1
        _FusionReactorLevel = -1
        _SolarSateliteLevel = -1

        _MetalCapacity = Double.NaN
        _CrystalCapacity = Double.NaN
        _DeuteriumCapacity = Double.NaN

        _MetalOverflowETA = Date.MinValue
        _CrystalOverflowETA = Date.MinValue
        _DeuteriumOverflowETA = Date.MinValue

    End Sub

    Public ReadOnly Property Planet() As Planet
        Get
            Return _Planet
        End Get
    End Property

#Region "delegated properties"

    Public ReadOnly Property ServerName() As String
        Get
            Return _Planet.ServerName
        End Get
    End Property

    Public ReadOnly Property Id() As String
        Get
            Return _Planet.Id
        End Get
    End Property

    Public ReadOnly Property OrdinalNumber() As Integer
        Get
            Return _Planet.OrdinalNumber
        End Get
    End Property

    Public ReadOnly Property SmallImageUri() As String
        Get
            Return _Planet.SmallImageUri
        End Get
    End Property

    Public ReadOnly Property Type() As String
        Get
            Return _Planet.Type
        End Get
    End Property

    Public ReadOnly Property MaxFields() As Integer
        Get
            Return _Planet.MaxFields
        End Get
    End Property

    Public ReadOnly Property LowestTemperature() As Integer
        Get
            Return _Planet.LowestTemperature
        End Get
    End Property

    Public ReadOnly Property HighestTemperature() As Integer
        Get
            Return _Planet.HighestTemperature
        End Get
    End Property

    Public ReadOnly Property GalaxyIndex() As Integer
        Get
            Return _Planet.GalaxyIndex
        End Get
    End Property

    Public ReadOnly Property SystemIndex() As Integer
        Get
            Return _Planet.SystemIndex
        End Get
    End Property

    Public ReadOnly Property PlanetIndex() As Integer
        Get
            Return _Planet.PlanetIndex
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _Planet.Name
        End Get
    End Property

    Public ReadOnly Property ServerTime() As String
        Get
            Return _Planet.ServerTime
        End Get
    End Property

    Public ReadOnly Property Points() As Integer
        Get
            Return _Planet.Points
        End Get
    End Property

    Public ReadOnly Property Rank() As Integer
        Get
            Return _Planet.Rank
        End Get
    End Property

    Public ReadOnly Property EmpireCount() As Integer
        Get
            Return _Planet.EmpireCount
        End Get
    End Property

    Public ReadOnly Property BuildingLevelMap() As Dictionary(Of Gid, Integer)
        Get
            Return _Planet.BuildingLevelMap
        End Get
    End Property

    Public ReadOnly Property ProductionMap() As Dictionary(Of Gid, Integer)
        Get
            Return _Planet.ProductionMap
        End Get
    End Property

    Public ReadOnly Property ResearchLevelMap() As Dictionary(Of Gid, Integer)
        Get
            Return _Planet.ResearchLevelMap
        End Get
    End Property

    Public ReadOnly Property PlanetaryDefenseMap() As Dictionary(Of Gid, Integer)
        Get
            Return _Planet.PlanetaryDefenseMap
        End Get
    End Property

    Public ReadOnly Property StationaryFleetMap() As Dictionary(Of Gid, Integer)
        Get
            Return _Planet.StationaryFleetMap
        End Get
    End Property

    Public ReadOnly Property ConstructionETA() As String
        Get
            Return _Planet.LocalTime + _Planet.ConstructionTimeLeft
        End Get
    End Property

    Public ReadOnly Property ResearchETA() As String
        Get
            Return _Planet.LocalTime + _Planet.ResearchTimeLeft
        End Get
    End Property

    Public ReadOnly Property LargeImageUri() As String
        Get
            Return _Planet.LargeImageUri
        End Get
    End Property

    Public ReadOnly Property Activity() As String
        Get
            Return _Planet.Activity
        End Get
    End Property

    Public ReadOnly Property HasMoon() As Boolean
        Get
            Return _Planet.HasMoon
        End Get
    End Property

    Public ReadOnly Property DebrisMetal() As Integer
        Get
            Return _Planet.DebrisMetal
        End Get
    End Property

    Public ReadOnly Property DebrisCrystal() As Integer
        Get
            Return _Planet.DebrisCrystal
        End Get
    End Property

    Public ReadOnly Property Username() As String
        Get
            Return _Planet.Username
        End Get
    End Property

    Public ReadOnly Property UserStatus() As String
        Get
            Return _Planet.UserStatus
        End Get
    End Property

    Public ReadOnly Property UserAlliance() As String
        Get
            Return _Planet.UserAlliance
        End Get
    End Property
#End Region

#Region "fields for data binding"

    Public Property LocalTime() As Date
        Get
            Return _LocalTime
        End Get
        Set(ByVal value As Date)
            _LocalTime = value
        End Set
    End Property

    Public ReadOnly Property Metal() As String
        Get
            If Double.IsNaN(_MetalProduction_0) OrElse Double.IsNaN(_MetalProduction_1) OrElse Double.IsNaN(_MetalCapacity) Then
                Metal = "n/a"
            Else
                Metal = CurrentMetal
            End If
        End Get
    End Property

    Public ReadOnly Property Crystal() As String
        Get
            If Double.IsNaN(_CrystalProduction_0) OrElse Double.IsNaN(_CrystalProduction_1) OrElse Double.IsNaN(_CrystalCapacity) Then
                Crystal = "n/a"
            Else
                Crystal = CurrentCrystal
            End If
        End Get
    End Property

    Public ReadOnly Property Deuterium() As String
        Get
            If Double.IsNaN(_DeuteriumProduction_0) OrElse Double.IsNaN(_DeuteriumProduction_1) OrElse Double.IsNaN(_DeuteriumCapacity) Then
                Deuterium = "n/a"
            Else
                Deuterium = CurrentDeuterium
            End If
        End Get
    End Property

    Public ReadOnly Property MetalProductionPotential() As String
        Get
            If Double.IsNaN(_MetalProductionPotential) Then
                MetalProductionPotential = "n/a"
            Else
                MetalProductionPotential = Fix(_MetalProductionPotential)
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalProductionPotential() As String
        Get
            If Double.IsNaN(_CrystalProductionPotential) Then
                CrystalProductionPotential = "n/a"
            Else
                CrystalProductionPotential = Fix(_CrystalProductionPotential)
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumProductionPotential() As String
        Get
            If Double.IsNaN(_DeuteriumProductionPotential) Then
                DeuteriumProductionPotential = "n/a"
            Else
                DeuteriumProductionPotential = Fix(_DeuteriumProductionPotential)
            End If
        End Get
    End Property

    Public ReadOnly Property MetalProduction() As String
        Get
            If Double.IsNaN(_MetalProduction_0) OrElse Double.IsNaN(_MetalProduction_1) Then
                MetalProduction = "n/a"
            ElseIf NotDeuteriumDepleted Then
                MetalProduction = Fix(_MetalProduction_0)
            Else
                MetalProduction = Fix(_MetalProduction_1)
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalProduction() As String
        Get
            If Double.IsNaN(_CrystalProduction_0) OrElse Double.IsNaN(_CrystalProduction_1) Then
                CrystalProduction = "n/a"
            ElseIf NotDeuteriumDepleted Then
                CrystalProduction = Fix(_CrystalProduction_0)
            Else
                CrystalProduction = Fix(_CrystalProduction_1)
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumProduction() As String
        Get
            If Double.IsNaN(_DeuteriumProduction_0) OrElse Double.IsNaN(_DeuteriumProduction_1) Then
                DeuteriumProduction = "n/a"
            ElseIf NotDeuteriumDepleted Then
                DeuteriumProduction = Fix(_DeuteriumProduction_0)
            Else
                DeuteriumProduction = Fix(_DeuteriumProduction_1)
            End If
        End Get
    End Property

    Public ReadOnly Property EnergyGenerationPotentialTotal() As String
        Get
            If Double.IsNaN(_EnergyGenerationPotentialTotal_0) OrElse Double.IsNaN(_EnergyGenerationPotentialTotal_1) Then
                EnergyGenerationPotentialTotal = "n/a"
            ElseIf NotDeuteriumDepleted Then
                EnergyGenerationPotentialTotal = Fix(_EnergyGenerationPotentialTotal_0)
            Else
                EnergyGenerationPotentialTotal = Fix(_EnergyGenerationPotentialTotal_1)
            End If
        End Get
    End Property

    Public ReadOnly Property EnergyConsumptionPotentialTotal() As String
        Get
            If Double.IsNaN(_EnergyConsumptionPotentialTotal) Then
                EnergyConsumptionPotentialTotal = "n/a"
            Else
                EnergyConsumptionPotentialTotal = Fix(_EnergyConsumptionPotentialTotal)
            End If
        End Get
    End Property

    Public ReadOnly Property ProductionFactor() As String
        Get
            If Double.IsNaN(_EnergyConsumptionPotentialTotal) Then
                ProductionFactor = "n/a"
            ElseIf _EnergyConsumptionPotentialTotal = 0 Then
                ProductionFactor = 0
            Else
                If NotDeuteriumDepleted Then
                    If Double.IsNaN(_EnergyGenerationPotentialTotal_0) Then
                        ProductionFactor = "n/a"
                    Else
                        ProductionFactor = Min(Round(_EnergyGenerationPotentialTotal_0 / _EnergyConsumptionPotentialTotal, 2), 1)
                    End If
                Else
                    If Double.IsNaN(_EnergyGenerationPotentialTotal_1) Then
                        ProductionFactor = "n/a"
                    Else
                        ProductionFactor = Min(Round(_EnergyGenerationPotentialTotal_1 / _EnergyConsumptionPotentialTotal, 2), 1)
                    End If
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property MetalMineEnergy() As String
        Get
            If Double.IsNaN(_MetalMineEnergyConsumption_0) OrElse Double.IsNaN(_MetalMineEnergyConsumption_1) Then
                MetalMineEnergy = "n/a"
            ElseIf NotDeuteriumDepleted Then
                MetalMineEnergy = Fix(_MetalMineEnergyConsumption_0)
            Else
                MetalMineEnergy = Fix(_MetalMineEnergyConsumption_1)
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalMineEnergy() As String
        Get
            If Double.IsNaN(_CrystalMineEnergyConsumption_0) OrElse Double.IsNaN(_CrystalMineEnergyConsumption_1) Then
                CrystalMineEnergy = "n/a"
            ElseIf NotDeuteriumDepleted Then
                CrystalMineEnergy = Fix(_CrystalMineEnergyConsumption_0)
            Else
                CrystalMineEnergy = Fix(_CrystalMineEnergyConsumption_1)
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumSynthesizerEnergy() As String
        Get
            If Double.IsNaN(_DeuteriumSynthesizerEnergyConsumption_0) OrElse Double.IsNaN(_DeuteriumSynthesizerEnergyConsumption_0) Then
                DeuteriumSynthesizerEnergy = "n/a"
            ElseIf NotDeuteriumDepleted Then
                DeuteriumSynthesizerEnergy = Fix(_DeuteriumSynthesizerEnergyConsumption_0)
            Else
                DeuteriumSynthesizerEnergy = Fix(_DeuteriumSynthesizerEnergyConsumption_1)
            End If
        End Get
    End Property

    Public ReadOnly Property EnergyConsumptionTotal() As String
        Get
            If Double.IsNaN(_EnergyConsumptionTotal_0) OrElse Double.IsNaN(_EnergyConsumptionTotal_1) Then
                EnergyConsumptionTotal = "n/a"
            ElseIf NotDeuteriumDepleted Then
                EnergyConsumptionTotal = Fix(_EnergyConsumptionTotal_0)
            Else
                EnergyConsumptionTotal = Fix(_EnergyConsumptionTotal_1)
            End If
        End Get
    End Property

    Public ReadOnly Property MetalMineEnergyConsumptionPotential() As String
        Get
            If Double.IsNaN(_MetalMineEnergyConsumptionPotential) Then
                MetalMineEnergyConsumptionPotential = "n/a"
            Else
                MetalMineEnergyConsumptionPotential = Fix(_MetalMineEnergyConsumptionPotential)
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalMineEnergyConsumptionPotential() As String
        Get
            If Double.IsNaN(_CrystalMineEnergyConsumptionPotential) Then
                CrystalMineEnergyConsumptionPotential = "n/a"
            Else
                CrystalMineEnergyConsumptionPotential = Fix(_CrystalMineEnergyConsumptionPotential)
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumSynthesizerEnergyConsumptionPotential() As String
        Get
            If Double.IsNaN(_DeuteriumSynthesizerEnergyConsumptionPotential) Then
                DeuteriumSynthesizerEnergyConsumptionPotential = "n/a"
            Else
                DeuteriumSynthesizerEnergyConsumptionPotential = Fix(_DeuteriumSynthesizerEnergyConsumptionPotential)
            End If
        End Get
    End Property

    Public ReadOnly Property SolarPlanetEnergy() As String
        Get
            If Double.IsNaN(_SolarPlanetEnergyGeneration) Then
                SolarPlanetEnergy = "n/a"
            Else
                SolarPlanetEnergy = Fix(_SolarPlanetEnergyGeneration)
            End If
        End Get
    End Property

    Public ReadOnly Property FusionReactorEnergy() As String
        Get
            If Double.IsNaN(_FusionReactorEnergyGeneration_0) OrElse Double.IsNaN(_FusionReactorEnergyGeneration_1) Then
                FusionReactorEnergy = "n/a"
            ElseIf NotDeuteriumDepleted Then
                FusionReactorEnergy = Fix(_FusionReactorEnergyGeneration_0)
            Else
                FusionReactorEnergy = Fix(_FusionReactorEnergyGeneration_1)
            End If
        End Get
    End Property

    Public ReadOnly Property SolarSateliteEnergy() As String
        Get
            If Double.IsNaN(_SolarSateliteEnergyGeneration) Then
                SolarSateliteEnergy = "n/a"
            Else
                SolarSateliteEnergy = Fix(_SolarSateliteEnergyGeneration)
            End If
        End Get
    End Property

    Public ReadOnly Property TotalEnergy() As String
        Get
            If Double.IsNaN(_MetalMineEnergyConsumption_0) OrElse Double.IsNaN(_CrystalMineEnergyConsumption_0) OrElse Double.IsNaN(_DeuteriumSynthesizerEnergyConsumption_0) OrElse _
                Double.IsNaN(_SolarPlanetEnergyGeneration) OrElse Double.IsNaN(_FusionReactorEnergyGeneration_0) OrElse Double.IsNaN(_SolarSateliteEnergyGeneration) Then
                TotalEnergy = "n/a"
            ElseIf NotDeuteriumDepleted Then
                TotalEnergy = Fix(-_MetalMineEnergyConsumption_0 + -_CrystalMineEnergyConsumption_0 + -_DeuteriumSynthesizerEnergyConsumption_0 + _SolarPlanetEnergyGeneration + _FusionReactorEnergyGeneration_0 + _SolarSateliteEnergyGeneration)
            Else
                TotalEnergy = Fix(-_MetalMineEnergyConsumption_1 + -_CrystalMineEnergyConsumption_1 + -_DeuteriumSynthesizerEnergyConsumption_1 + _SolarPlanetEnergyGeneration + _FusionReactorEnergyGeneration_1 + _SolarSateliteEnergyGeneration)
            End If
        End Get
    End Property

    Public ReadOnly Property MetalCapacity() As String
        Get
            If Double.IsNaN(_MetalCapacity) Then
                MetalCapacity = "n/a"
            Else
                MetalCapacity = CInt(_MetalCapacity)
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalCapacity() As String
        Get
            If Double.IsNaN(_CrystalCapacity) Then
                CrystalCapacity = "n/a"
            Else
                CrystalCapacity = CInt(_CrystalCapacity)
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumCapacity() As String
        Get
            If Double.IsNaN(_DeuteriumCapacity) Then
                DeuteriumCapacity = "n/a"
            Else
                DeuteriumCapacity = CInt(_DeuteriumCapacity)
            End If
        End Get
    End Property

    Public ReadOnly Property MetalStoragePercentage() As String
        Get
            If Double.IsNaN(_MetalCapacity) OrElse Double.IsNaN(_MetalProduction_0) OrElse Double.IsNaN(_MetalCapacity) Then
                MetalStoragePercentage = "n/a"
            Else
                MetalStoragePercentage = CInt(CurrentMetal / _MetalCapacity / 10)
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalStoragePercentage() As String
        Get
            If Double.IsNaN(_CrystalCapacity) OrElse Double.IsNaN(_CrystalProduction_0) OrElse Double.IsNaN(_CrystalCapacity) Then
                CrystalStoragePercentage = "n/a"
            Else
                CrystalStoragePercentage = CInt(CurrentCrystal / _CrystalCapacity / 10)
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumStoragePercentage() As String
        Get
            If Double.IsNaN(_DeuteriumCapacity) OrElse Double.IsNaN(_DeuteriumProduction_0) OrElse Double.IsNaN(_DeuteriumCapacity) Then
                DeuteriumStoragePercentage = "n/a"
            Else
                DeuteriumStoragePercentage = CInt(CurrentDeuterium / _DeuteriumCapacity / 10)
            End If
        End Get
    End Property

    Public ReadOnly Property MetalOverflowETA() As String
        Get
            'Return EstimateTime(_Planet.Metal, _MetalProduction_0, _MetalCapacity)
            If _MetalOverflowETA = Date.MinValue Then
                MetalOverflowETA = "n/a"
            Else
                MetalOverflowETA = _MetalOverflowETA
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalOverflowETA() As String
        Get
            'Return EstimateTime(_Planet.Crystal, _CrystalProduction_0, _CrystalCapacity)
            If _CrystalOverflowETA = Date.MinValue Then
                CrystalOverflowETA = "n/a"
            Else
                CrystalOverflowETA = _CrystalOverflowETA
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumOverflowETA() As String
        Get
            'Return EstimateTime(_Planet.Deuterium, _DeuteriumProduction_0, _DeuteriumCapacity)
            If _DeuteriumOverflowETA = Date.MinValue Then
                DeuteriumOverflowETA = "n/a"
            Else
                DeuteriumOverflowETA = _DeuteriumOverflowETA
            End If
        End Get
    End Property

    Public ReadOnly Property Total() As String
        Get
            If Double.IsNaN(_MetalProduction_0) OrElse Double.IsNaN(_CrystalProduction_0) OrElse Double.IsNaN(_DeuteriumProduction_0) OrElse _
                Double.IsNaN(_MetalCapacity) OrElse Double.IsNaN(_CrystalCapacity) OrElse Double.IsNaN(_DeuteriumCapacity) Then
                Total = "n/a"
            Else
                Total = GetTotal()
            End If
        End Get
    End Property

    'Public ReadOnly Property TotalCapacity() As String
    '    Get
    '        If Double.IsNaN(_MetalCapacity) OrElse Double.IsNaN(_CrystalCapacity) OrElse Double.IsNaN(_DeuteriumCapacity) Then
    '            TotalCapacity = "n/a"
    '        Else
    '            TotalCapacity = CInt(GetTotalCapacity())
    '        End If
    '    End Get
    'End Property

    Public ReadOnly Property TotalProduction() As String
        Get
            If Double.IsNaN(_MetalProduction_0) OrElse Double.IsNaN(_CrystalProduction_0) OrElse Double.IsNaN(_DeuteriumProduction_0) Then
                TotalProduction = "n/a"
            Else
                TotalProduction = CInt(GetTotalProduction())
            End If
        End Get
    End Property

    'Public ReadOnly Property TotalStoragePercentage() As String
    '    Get
    '        If Double.IsNaN(_MetalCapacity) OrElse Double.IsNaN(_CrystalCapacity) OrElse Double.IsNaN(_DeuteriumCapacity) Then
    '            TotalStoragePercentage = "n/a"
    '        Else
    '            TotalStoragePercentage = CInt(GetTotal() / GetTotalCapacity() * 100)
    '        End If
    '    End Get
    'End Property

    'Public ReadOnly Property TotalOverflowETA() As String
    '    Get
    '        If Double.IsNaN(_MetalCapacity) OrElse Double.IsNaN(_CrystalCapacity) OrElse Double.IsNaN(_DeuteriumCapacity) OrElse _
    '            Double.IsNaN(_MetalProduction) OrElse Double.IsNaN(_CrystalProduction) OrElse Double.IsNaN(_DeuteriumProduction) Then
    '            TotalOverflowETA = "n/a"
    '        Else
    '            TotalOverflowETA = EstimateTime(GetTotal(), GetTotalProduction(), GetTotalCapacity())
    '        End If
    '    End Get
    'End Property

    Public ReadOnly Property MetalMineProductionPercentage() As String
        Get
            If _MetalMineProductionPercentage = -1 Then
                MetalMineProductionPercentage = "n/a"
            Else
                MetalMineProductionPercentage = _MetalMineProductionPercentage
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalMineProductionPercentage() As String
        Get
            If _CrystalMineProductionPercentage = -1 Then
                CrystalMineProductionPercentage = "n/a"
            Else
                CrystalMineProductionPercentage = _CrystalMineProductionPercentage
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumSynthesizerProductionPercentage() As String
        Get
            If _DeuteriumSynthesizerProductionPercentage = -1 Then
                DeuteriumSynthesizerProductionPercentage = "n/a"
            Else
                DeuteriumSynthesizerProductionPercentage = _DeuteriumSynthesizerProductionPercentage
            End If
        End Get
    End Property

    Public ReadOnly Property SolarPlanetProductionPercentage() As String
        Get
            If _SolarPlanetProductionPercentage = -1 Then
                SolarPlanetProductionPercentage = "n/a"
            Else
                SolarPlanetProductionPercentage = _SolarPlanetProductionPercentage
            End If
        End Get
    End Property

    Public ReadOnly Property FusionReactorProductionPercentage() As String
        Get
            If _FusionReactorProductionPercentage = -1 Then
                FusionReactorProductionPercentage = "n/a"
            Else
                FusionReactorProductionPercentage = _FusionReactorProductionPercentage
            End If
        End Get
    End Property

    Public ReadOnly Property SolarSateliteProductionPercentage() As String
        Get
            If _SolarSateliteProductionPercentage = -1 Then
                SolarSateliteProductionPercentage = "n/a"
            Else
                SolarSateliteProductionPercentage = _SolarSateliteProductionPercentage
            End If
        End Get
    End Property

    'Public ReadOnly Property TotalProductionPercentage() As String
    '    Get
    '        If _MetalMineProductionPercentage = -1 OrElse _CrystalMineProductionPercentage = -1 OrElse _DeuteriumSynthesizerProductionPercentage = -1 OrElse _
    '            _SolarPlanetProductionPercentage = -1 OrElse _FusionReactorProductionPercentage = -1 OrElse _SolarSateliteProductionPercentage = -1 Then
    '            TotalProductionPercentage = "n/a"
    '        Else
    '            TotalProductionPercentage = (_MetalMineProductionPercentage + _CrystalMineProductionPercentage + _DeuteriumSynthesizerProductionPercentage + _SolarPlanetProductionPercentage + _FusionReactorProductionPercentage + _SolarSateliteProductionPercentage) / 6
    '        End If
    '    End Get
    'End Property

    Public ReadOnly Property MetalMineLevel() As String
        Get
            If _MetalMineLevel = -1 Then
                MetalMineLevel = "n/a"
            Else
                MetalMineLevel = _MetalMineLevel
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalMineLevel() As String
        Get
            If _CrystalMineLevel = -1 Then
                CrystalMineLevel = "n/a"
            Else
                CrystalMineLevel = _CrystalMineLevel
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumSynthesizerLevel() As String
        Get
            If _DeuteriumSynthesizerLevel = -1 Then
                DeuteriumSynthesizerLevel = "n/a"
            Else
                DeuteriumSynthesizerLevel = _DeuteriumSynthesizerLevel
            End If
        End Get
    End Property

    Public ReadOnly Property SolarPlanetLevel() As String
        Get
            If _SolarPlanetLevel = -1 Then
                SolarPlanetLevel = "n/a"
            Else
                SolarPlanetLevel = _SolarPlanetLevel
            End If
        End Get
    End Property

    Public ReadOnly Property FusionReactorLevel() As String
        Get
            If _FusionReactorLevel = -1 Then
                FusionReactorLevel = "n/a"
            Else
                FusionReactorLevel = _FusionReactorLevel
            End If
        End Get
    End Property

    Public ReadOnly Property SolarSateliteLevel() As String
        Get
            If _SolarSateliteLevel = -1 Then
                SolarSateliteLevel = "n/a"
            Else
                SolarSateliteLevel = _SolarSateliteLevel
            End If
        End Get
    End Property
#End Region

    Private Sub ChangedEventHandler()

        Dim buildingLevels As Dictionary(Of Gid, Integer) = _Planet.BuildingLevelMap
        If buildingLevels Is Nothing Then
            Exit Sub
        End If

        'begin: calculate capacities

        'metal
        If buildingLevels.ContainsKey(Gid.MetalStorage) Then
            _MetalStorageLevel = buildingLevels(Gid.MetalStorage)
        Else
            _MetalStorageLevel = 0
        End If
        _MetalCapacity = CalculateCapacity(_MetalStorageLevel)

        'crystal
        If buildingLevels.ContainsKey(Gid.CrystalStorage) Then
            _CrystalStorageLevel = buildingLevels(Gid.CrystalStorage)
        Else
            _CrystalStorageLevel = 0
        End If
        _CrystalCapacity = CalculateCapacity(_CrystalStorageLevel)

        'deuterium
        If buildingLevels.ContainsKey(Gid.DeuteriumTank) Then
            _DeuteriumTankLevel = buildingLevels(Gid.DeuteriumTank)
        Else
            _DeuteriumTankLevel = 0
        End If
        _DeuteriumCapacity = CalculateCapacity(_DeuteriumTankLevel)

        'end: calculate capacities

        Dim _workloads As Dictionary(Of Gid, Integer) = _Planet.ProductionMap
        If _workloads Is Nothing Then
            Exit Sub
        End If

        Dim highestTemperature As Integer = _Planet.HighestTemperature

        'begin: calculate production and power consumption

        'metal mine
        If buildingLevels.ContainsKey(Gid.MetalMine) AndAlso _workloads.ContainsKey(Gid.MetalMine) Then
            Dim level As Integer = buildingLevels(Gid.MetalMine)
            Dim percentage As Integer = _workloads(Gid.MetalMine)

            _MetalProductionPotential = 30 * level * 1.1 ^ level * percentage / 100
            _MetalMineEnergyConsumptionPotential = 10 * level * 1.1 ^ level * percentage / 100
            _MetalMineLevel = level
            _MetalMineProductionPercentage = percentage
        Else
            _MetalProductionPotential = 0
            _MetalMineEnergyConsumptionPotential = 0
            _MetalMineLevel = 0
            _MetalMineProductionPercentage = 0
        End If

        'crystal mine
        If buildingLevels.ContainsKey(Gid.CrystalMine) AndAlso _workloads.ContainsKey(Gid.CrystalMine) Then
            Dim level As Integer = buildingLevels(Gid.CrystalMine)
            Dim percentage As Integer = _workloads(Gid.CrystalMine)

            _CrystalProductionPotential = 20 * level * 1.1 ^ level * percentage / 100
            _CrystalMineEnergyConsumptionPotential = 10 * level * 1.1 ^ level * percentage / 100
            _CrystalMineLevel = level
            _CrystalMineProductionPercentage = percentage
        Else
            _CrystalProductionPotential = 0
            _CrystalMineEnergyConsumptionPotential = 0
            _CrystalMineLevel = 0
            _CrystalMineProductionPercentage = 0
        End If

        'deuterium synthesizer
        If buildingLevels.ContainsKey(Gid.DeuteriumSynthesizer) AndAlso _workloads.ContainsKey(Gid.DeuteriumSynthesizer) Then
            Dim level As Integer = buildingLevels(Gid.DeuteriumSynthesizer)
            Dim percentage As Integer = _workloads(Gid.DeuteriumSynthesizer)

            _DeuteriumProductionPotential = 10 * level * 1.1 ^ level * (-0.002 * highestTemperature + 1.28) * percentage / 100
            _DeuteriumSynthesizerEnergyConsumptionPotential = 20 * level * 1.1 ^ level * percentage / 100
            _DeuteriumSynthesizerLevel = level
            _DeuteriumSynthesizerProductionPercentage = percentage
        Else
            _DeuteriumProductionPotential = 0
            _DeuteriumSynthesizerEnergyConsumptionPotential = 0
            _DeuteriumSynthesizerLevel = 0
            _DeuteriumSynthesizerProductionPercentage = 0
        End If

        'end: calculate production and power consumption

        'begin: calculate power generation and deuterium consumption

        'solar planet
        If buildingLevels.ContainsKey(Gid.SolarPlant) AndAlso _workloads.ContainsKey(Gid.SolarPlant) Then
            Dim level As Integer = buildingLevels(Gid.SolarPlant)
            Dim percentage As Integer = _workloads(Gid.SolarPlant)

            _SolarPlanetEnergyGeneration = 20 * level * 1.1 ^ level * percentage / 100
            _SolarPlanetLevel = level
            _SolarPlanetProductionPercentage = percentage
        Else
            _SolarPlanetEnergyGeneration = 0
            _SolarPlanetLevel = 0
            _SolarPlanetProductionPercentage = 0
        End If

        'fusion planet (_DeuteriumProduction is valid)
        If buildingLevels.ContainsKey(Gid.FusionReactor) AndAlso _workloads.ContainsKey(Gid.FusionReactor) Then
            Dim level As Integer = buildingLevels(Gid.FusionReactor)
            Dim percentage As Integer = _workloads(Gid.FusionReactor)

            _FusionReactorEnergyGenerationPotential = 50 * level * 1.1 ^ level * percentage / 100
            _FusionReactorLevel = level
            _FusionReactorProductionPercentage = percentage

            '_DeuteriumProduction -= 10 * level * 1.1 ^ level * (-0.002 * _HighestTemperature + 1.28) * percentage / 100
            _DeuteriumConsumptionPotential = 10 * level * 1.1 ^ level '* (-0.002 * _HighestTemperature + 1.28) * percentage / 100
        Else
            _FusionReactorEnergyGenerationPotential = 0
            _FusionReactorLevel = 0
            _FusionReactorProductionPercentage = 0

            _DeuteriumConsumptionPotential = 0
        End If

        Dim stationaryFleet As Dictionary(Of Gid, Integer) = _Planet.StationaryFleetMap
        If stationaryFleet Is Nothing Then
            Exit Sub
        End If

        'solar satelites
        If stationaryFleet.ContainsKey(Gid.SolarSatellite) AndAlso _workloads.ContainsKey(Gid.SolarSatellite) Then
            Dim level As Integer = stationaryFleet(Gid.SolarSatellite)
            Dim percentage As Integer = _workloads(Gid.SolarSatellite)

            _SolarSateliteEnergyGeneration = Min(highestTemperature / 4 + 20, 50) * level * percentage / 100
            _SolarSateliteLevel = level
            _SolarSateliteProductionPercentage = percentage
        Else
            _SolarSateliteEnergyGeneration = 0
            _SolarSateliteLevel = 0
            _SolarSateliteProductionPercentage = 0
        End If

        'end: calculate power generation and deuterium consumption

        _EnergyConsumptionPotentialTotal = _MetalMineEnergyConsumptionPotential + _CrystalMineEnergyConsumptionPotential + _DeuteriumSynthesizerEnergyConsumptionPotential

        Dim hoursToDeuteriumDeplete As Double
        If _DeuteriumConsumptionPotential > _DeuteriumProductionPotential Then
            hoursToDeuteriumDeplete = _Planet.Deuterium / (_DeuteriumConsumptionPotential - _DeuteriumProductionPotential)
            _DeuteriumDepletedETA = _Planet.LocalTime + New TimeSpan(0, 0, hoursToDeuteriumDeplete * 3600)

            _Metal_D = _Planet.Metal + _MetalProduction_0 * hoursToDeuteriumDeplete
            _Crystal_D = _Planet.Crystal + _CrystalProduction_0 * hoursToDeuteriumDeplete
            _Deuterium_D = _Planet.Deuterium + _DeuteriumProduction_0 * hoursToDeuteriumDeplete
        ElseIf _DeuteriumProductionPotential = 0 AndAlso _Planet.Deuterium = 0 Then
            hoursToDeuteriumDeplete = 0
            _DeuteriumDepletedETA = _Planet.LocalTime

            _Metal_D = _Planet.Metal
            _Crystal_D = _Planet.Crystal
            _Deuterium_D = 0
        Else
            hoursToDeuteriumDeplete = Double.MaxValue
            _DeuteriumDepletedETA = Date.MaxValue

            _Metal_D = Double.MaxValue
            _Crystal_D = Double.MaxValue
            _Deuterium_D = Double.MaxValue
        End If

        _FusionReactorEnergyGeneration_0 = _FusionReactorEnergyGenerationPotential
        'begin: _FusionReactorEnergyGeneration_1
        Dim solarEnergyGeneration = _SolarSateliteEnergyGeneration + _SolarSateliteEnergyGeneration
        If solarEnergyGeneration > 0 AndAlso _DeuteriumProductionPotential > 0 Then
            Dim r As Double = (_EnergyConsumptionPotentialTotal * _DeuteriumConsumptionPotential / _DeuteriumProductionPotential - _FusionReactorEnergyGenerationPotential) / solarEnergyGeneration
            _FusionReactorEnergyGeneration_1 = _FusionReactorEnergyGenerationPotential / r
        Else
            _FusionReactorEnergyGeneration_1 = 0
        End If
        'end: _FusionReactorEnergyGeneration_1

        _EnergyGenerationPotentialTotal_0 = _SolarPlanetEnergyGeneration + _FusionReactorEnergyGeneration_0 + _SolarSateliteEnergyGeneration
        _EnergyGenerationPotentialTotal_1 = _SolarPlanetEnergyGeneration + _FusionReactorEnergyGeneration_1 + _SolarSateliteEnergyGeneration

        'adjust consumption and production
        If _EnergyConsumptionPotentialTotal > _EnergyGenerationPotentialTotal_0 Then
            Dim k As Double = _EnergyGenerationPotentialTotal_0 / _EnergyConsumptionPotentialTotal
            _MetalProduction_0 = _MetalProductionPotential * k + 20
            _CrystalProduction_0 = _CrystalProductionPotential * k + 10
            _DeuteriumProduction_0 = _DeuteriumProductionPotential * k - _DeuteriumConsumptionPotential

            _MetalMineEnergyConsumption_0 = _MetalMineEnergyConsumptionPotential * k
            _CrystalMineEnergyConsumption_0 = _CrystalMineEnergyConsumptionPotential * k
            _DeuteriumSynthesizerEnergyConsumption_0 = _DeuteriumSynthesizerEnergyConsumptionPotential * k
        Else
            _MetalProduction_0 = _MetalProductionPotential + 20
            _CrystalProduction_0 = _CrystalProductionPotential + 10
            _DeuteriumProduction_0 = _DeuteriumProductionPotential - _DeuteriumConsumptionPotential

            _MetalMineEnergyConsumption_0 = _MetalMineEnergyConsumptionPotential
            _CrystalMineEnergyConsumption_0 = _CrystalMineEnergyConsumptionPotential
            _DeuteriumSynthesizerEnergyConsumption_0 = _DeuteriumSynthesizerEnergyConsumptionPotential
        End If
        If _EnergyConsumptionPotentialTotal > _EnergyGenerationPotentialTotal_1 Then
            Dim k As Double = _EnergyGenerationPotentialTotal_1 / _EnergyConsumptionPotentialTotal
            _MetalProduction_1 = _MetalProductionPotential * k + 20
            _CrystalProduction_1 = _CrystalProductionPotential * k + 10
            _DeuteriumProduction_1 = _DeuteriumProductionPotential * k - _DeuteriumConsumptionPotential

            _MetalMineEnergyConsumption_1 = _MetalMineEnergyConsumptionPotential * k
            _CrystalMineEnergyConsumption_1 = _CrystalMineEnergyConsumptionPotential * k
            _DeuteriumSynthesizerEnergyConsumption_1 = _DeuteriumSynthesizerEnergyConsumptionPotential * k
        Else
            _MetalProduction_1 = _MetalProductionPotential + 20
            _CrystalProduction_1 = _CrystalProductionPotential + 10
            _DeuteriumProduction_1 = _DeuteriumProductionPotential - _DeuteriumConsumptionPotential

            _MetalMineEnergyConsumption_1 = _MetalMineEnergyConsumptionPotential
            _CrystalMineEnergyConsumption_1 = _CrystalMineEnergyConsumptionPotential
            _DeuteriumSynthesizerEnergyConsumption_1 = _DeuteriumSynthesizerEnergyConsumptionPotential
        End If

        _EnergyConsumptionTotal_0 = _MetalMineEnergyConsumption_0 + _CrystalMineEnergyConsumption_0 + _DeuteriumSynthesizerEnergyConsumption_0
        _EnergyConsumptionTotal_1 = _MetalMineEnergyConsumption_1 + _CrystalMineEnergyConsumption_1 + _DeuteriumSynthesizerEnergyConsumption_1

        If _Metal_D < _MetalCapacity Then
            _MetalOverflowETA = _Planet.LocalTime + New TimeSpan(0, 0, 3600 * hoursToDeuteriumDeplete) + New TimeSpan(0, 0, 3600 * (1000 * _MetalCapacity - _Metal_D) / _MetalProduction_1)
        Else
            _MetalOverflowETA = _Planet.LocalTime + New TimeSpan(0, 0, 3600 * (1000 * _MetalCapacity - _Planet.Metal) / _MetalProduction_0)
        End If
        If _Crystal_D < _CrystalCapacity Then
            _CrystalOverflowETA = _Planet.LocalTime + New TimeSpan(0, 0, 3600 * hoursToDeuteriumDeplete) + New TimeSpan(0, 0, 3600 * (1000 * _CrystalCapacity - _Crystal_D) / _CrystalProduction_1)
        Else
            _CrystalOverflowETA = _Planet.LocalTime + New TimeSpan(0, 0, 3600 * (1000 * _CrystalCapacity - _Planet.Crystal) / _CrystalProduction_0)
        End If
        If _Deuterium_D < _DeuteriumCapacity Then
            If _DeuteriumProduction_1 > 0 Then
                _DeuteriumOverflowETA = _Planet.LocalTime + New TimeSpan(0, 0, 3600 * hoursToDeuteriumDeplete) + New TimeSpan(0, 0, 3600 * (1000 * _DeuteriumCapacity - _Deuterium_D) / _DeuteriumProduction_1)
            Else
                _DeuteriumOverflowETA = Date.MaxValue
            End If
        Else
            If _DeuteriumProduction_0 > 0 Then
                _DeuteriumOverflowETA = _Planet.LocalTime + New TimeSpan(0, 0, 3600 * (1000 * _DeuteriumCapacity - _Planet.Deuterium) / _DeuteriumProduction_0)
            Else
                _DeuteriumOverflowETA = Date.MaxValue
            End If
        End If
    End Sub

    Private Function CalculateCapacity(ByVal level As Integer) As Integer

        Dim cap As Integer

        Select Case level
            Case 0 : cap = 100
            Case 1 : cap = 150
            Case 2 : cap = 200
            Case 3 : cap = 300
            Case 4 : cap = 400
            Case 5 : cap = 600
            Case 6 : cap = 900
            Case 7 : cap = 1400
            Case 8 : cap = 2200
            Case 9 : cap = 3500
            Case 10 : cap = 5550
            Case 11 : cap = 8850
            Case 12 : cap = 14150
            Case 13 : cap = 22600
            Case 14 : cap = 36100
            Case Else
                Throw New NotImplementedException("CalculateCapacity not defined for level: " & level)
        End Select

        Return cap

    End Function

    Private Function GetTotal() As Integer

        Return CurrentMetal + CurrentCrystal + CurrentDeuterium

    End Function

    'Private Function GetTotalCapacity() As Double

    '    Return _MetalCapacity + _CrystalCapacity + _DeuteriumCapacity

    'End Function

    Private Function GetTotalProduction() As Double

        Return _MetalProduction_0 + _CrystalProduction_0 + _DeuteriumProduction_0

    End Function

    'Private Function EstimateTime(ByVal stock As Integer, ByVal growth As Double, ByVal capacity As Double) As String

    '    If Double.IsNaN(growth) OrElse Double.IsNaN(capacity) Then
    '        EstimateTime = "n/a"
    '    ElseIf growth <= 0 Then
    '        EstimateTime = "never"
    '    Else
    '        Dim w As Single = (capacity - stock) / growth / 24 / 7
    '        Dim wi As Integer = Floor(w)
    '        If wi > 0 Then
    '            Return "more than " & wi & " week(s)"
    '        End If

    '        Dim d As Single = (w - wi) * 7
    '        Dim di As Integer = Floor(d)
    '        If di > 0 Then
    '            Return "more than " & di & " day(s)"
    '        End If

    '        Dim h As Single = (d - di) * 24
    '        Dim hi As Integer = Floor(h)
    '        Dim m As Single = (h - hi) * 60
    '        Dim mi As Integer = Floor(m)
    '        Dim s As Single = (m - mi) * 60
    '        Dim si As Integer = Floor(s)

    '        EstimateTime = hi & "h " & mi & "m " & si & "s"
    '    End If
    'End Function

    Private ReadOnly Property CurrentMetal() As Integer
        Get
            Dim p As Integer
            If NotDeuteriumDepleted Then
                p = Fix(_Planet.Metal + _MetalProduction_0 * (_LocalTime - _Planet.LocalTime).TotalHours)
            Else
                p = Fix(_Metal_D + _MetalProduction_1 * (_LocalTime - _DeuteriumDepletedETA).TotalHours)
            End If
            Return Min(p, _MetalCapacity * 1000)
        End Get
    End Property

    Private ReadOnly Property CurrentCrystal() As Integer
        Get
            Dim p As Integer
            If NotDeuteriumDepleted Then
                p = Fix(_Planet.Crystal + _CrystalProduction_0 * (_LocalTime - _Planet.LocalTime).TotalHours)
            Else
                p = Fix(_Crystal_D + _CrystalProduction_1 * (_LocalTime - _DeuteriumDepletedETA).TotalHours)
            End If
            Return Min(p, _CrystalCapacity * 1000)
        End Get
    End Property

    Private ReadOnly Property CurrentDeuterium() As Integer
        Get
            Dim p As Integer
            If NotDeuteriumDepleted Then
                p = Fix(_Planet.Deuterium + _DeuteriumProduction_0 * (_LocalTime - _Planet.LocalTime).TotalHours)
            Else
                p = Fix(_Deuterium_D + _DeuteriumProduction_1 * (_LocalTime - _DeuteriumDepletedETA).TotalHours)
            End If
            Return Min(Max(p, 0), _DeuteriumCapacity * 1000)
        End Get
    End Property

    Private ReadOnly Property NotDeuteriumDepleted() As Boolean
        Get
            Return _LocalTime < _DeuteriumDepletedETA
        End Get
    End Property

    '<DataObjectMethod(DataObjectMethodType.Select, True)> _
    'Public Function GetUpgradeCommands() As ICollection(Of Command.IUpgradeCommand)

    '    'Return _Planet.UpgradeCommandList.GetRange(startRowIndex, maximumRows)
    '    Return _Planet.UpgradeCommandMap.Values

    'End Function

    ''Public Function CountUpgradeCommand(ByVal startRowIndex As Integer, ByVal maximumRows As Integer) As Integer

    ''    Return _Planet.UpgradeCommandMap.Count

    ''End Function

    <DataObjectMethod(DataObjectMethodType.Select, True)> _
    Public Function GetConstructionCommands() As ICollection(Of Command.ConstructCommand)

        Return _Planet.ConstructionCommandDictionary.Values

    End Function

    <DataObjectMethod(DataObjectMethodType.Select, True)> _
    Public Function GetResearchCommands() As ICollection(Of Command.ResearchCommand)

        Return _Planet.ResearchCommandDictionary.Values

    End Function
End Class
