Imports System.Math

Public Class PlanetView

#Region "variables"

    Private ReadOnly _Planet As Planet

    Private _MetalProduction As Double
    Private _CrystalProduction As Double
    Private _DeuteriumProduction As Double
    Private _DeuteriumConsumption As Double

    Private _PowerGeneration As Double
    Private _PowerConsumption As Double

    Private _MetalMineEnergy As Double
    Private _CrystalMineEnergy As Double
    Private _DeuteriumSynthesizerEnergy As Double
    Private _SolarPlanetEnergy As Double
    Private _FusionReactorEnergy As Double
    Private _SolarSateliteEnergy As Double

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

    Private _MetalCapacity As Double
    Private _CrystalCapacity As Double
    Private _DeuteriumCapacity As Double

    Private _MetalProductionPotential As Double
    Private _CrystalProductionPotential As Double
    Private _DeuteriumProductionPotential As Double
    Private _MetalMineEnergyPotential As Double
    Private _CrystalMineEnergyPotential As Double
    Private _DeuteriumSynthesizerEnergyPotential As Double

#End Region

    Public Sub New(ByVal p As Planet)

        _Planet = p
        AddHandler p.Changed, AddressOf ChangedEventHandler

        _MetalProduction = Double.NaN
        _CrystalProduction = Double.NaN
        _DeuteriumProduction = Double.NaN
        _DeuteriumConsumption = Double.NaN

        _PowerGeneration = Double.NaN
        _PowerConsumption = Double.NaN

        _MetalMineEnergy = Double.NaN
        _CrystalMineEnergy = Double.NaN
        _DeuteriumSynthesizerEnergy = Double.NaN
        _SolarPlanetEnergy = Double.NaN
        _FusionReactorEnergy = Double.NaN
        _SolarSateliteEnergy = Double.NaN

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

    Public ReadOnly Property LocalTime() As Date
        Get
            Return _Planet.LocalTime
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

#Region "properties"

    Public ReadOnly Property Metal() As String
        Get
            If Double.IsNaN(_MetalProduction) OrElse Double.IsNaN(_MetalCapacity) Then
                Metal = "n/a"
            Else
                Metal = CurrentMetal
            End If
        End Get
    End Property

    Public ReadOnly Property Crystal() As String
        Get
            If Double.IsNaN(_CrystalProduction) OrElse Double.IsNaN(_CrystalCapacity) Then
                Crystal = "n/a"
            Else
                Crystal = CurrentCrystal
            End If
        End Get
    End Property

    Public ReadOnly Property Deuterium() As String
        Get
            If Double.IsNaN(_DeuteriumProduction) OrElse Double.IsNaN(_DeuteriumCapacity) Then
                Deuterium = "n/a"
            Else
                Deuterium = CurrentDeuterium
            End If
        End Get
    End Property

    Public ReadOnly Property MetalProduction() As String
        Get
            If Double.IsNaN(_MetalProduction) Then
                MetalProduction = "n/a"
            Else
                MetalProduction = Fix(_MetalProduction)
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalProduction() As String
        Get
            If Double.IsNaN(_CrystalProduction) Then
                CrystalProduction = "n/a"
            Else
                CrystalProduction = Fix(_CrystalProduction)
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumProduction() As String
        Get
            If Double.IsNaN(_DeuteriumProduction) Then
                DeuteriumProduction = "n/a"
            ElseIf _DeuteriumProduction < 0 AndAlso Deuterium = 0 Then
                DeuteriumProduction = 0
            Else
                DeuteriumProduction = Fix(_DeuteriumProduction)
            End If
        End Get
    End Property

    Public ReadOnly Property PowerGeneration() As String
        Get
            If Double.IsNaN(_PowerGeneration) Then
                PowerGeneration = "n/a"
            ElseIf _DeuteriumProduction >= 0 OrElse Deuterium > 0 Then
                PowerGeneration = Fix(_PowerGeneration)
            Else
                PowerGeneration = Fix(_PowerGeneration - _FusionReactorEnergy + _FusionReactorEnergy * _DeuteriumProduction / _DeuteriumConsumption)
            End If
        End Get
    End Property

    Public ReadOnly Property PowerConsumption() As String
        Get
            If Double.IsNaN(_PowerConsumption) Then
                PowerConsumption = "n/a"
            Else
                PowerConsumption = Fix(_PowerConsumption)
            End If
        End Get
    End Property

    Public ReadOnly Property ProductionFactor() As String
        Get
            If Double.IsNaN(_PowerGeneration) OrElse Double.IsNaN(_PowerConsumption) Then
                ProductionFactor = "n/a"
            ElseIf _PowerConsumption = 0 Then
                ProductionFactor = 0
            Else
                ProductionFactor = Min(Round(_PowerGeneration / _PowerConsumption, 2), 1)
            End If
        End Get
    End Property

    Public ReadOnly Property MetalMineEnergy() As String
        Get
            If Double.IsNaN(_MetalMineEnergy) Then
                MetalMineEnergy = "n/a"
            Else
                MetalMineEnergy = Fix(_MetalMineEnergy)
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalMineEnergy() As String
        Get
            If Double.IsNaN(_CrystalMineEnergy) Then
                CrystalMineEnergy = "n/a"
            Else
                CrystalMineEnergy = Fix(_CrystalMineEnergy)
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumSynthesizerEnergy() As String
        Get
            If Double.IsNaN(_DeuteriumSynthesizerEnergy) Then
                DeuteriumSynthesizerEnergy = "n/a"
            Else
                DeuteriumSynthesizerEnergy = Fix(_DeuteriumSynthesizerEnergy)
            End If
        End Get
    End Property

    Public ReadOnly Property SolarPlanetEnergy() As String
        Get
            If Double.IsNaN(_SolarPlanetEnergy) Then
                SolarPlanetEnergy = "n/a"
            Else
                SolarPlanetEnergy = Fix(_SolarPlanetEnergy)
            End If
        End Get
    End Property

    Public ReadOnly Property FusionReactorEnergy() As String
        Get
            If Double.IsNaN(_FusionReactorEnergy) Then
                FusionReactorEnergy = "n/a"
            Else
                FusionReactorEnergy = Fix(_FusionReactorEnergy)
            End If
        End Get
    End Property

    Public ReadOnly Property SolarSateliteEnergy() As String
        Get
            If Double.IsNaN(_SolarSateliteEnergy) Then
                SolarSateliteEnergy = "n/a"
            Else
                SolarSateliteEnergy = Fix(_SolarSateliteEnergy)
            End If
        End Get
    End Property

    Public ReadOnly Property TotalEnergy() As String
        Get
            If Double.IsNaN(_MetalMineEnergy) OrElse Double.IsNaN(_CrystalMineEnergy) OrElse Double.IsNaN(_DeuteriumSynthesizerEnergy) OrElse _
                Double.IsNaN(_SolarPlanetEnergy) OrElse Double.IsNaN(_FusionReactorEnergy) OrElse Double.IsNaN(_SolarSateliteEnergy) Then
                TotalEnergy = "n/a"
            Else
                TotalEnergy = Fix(_MetalMineEnergy + _CrystalMineEnergy + _DeuteriumSynthesizerEnergy + _SolarPlanetEnergy + _FusionReactorEnergy + _SolarSateliteEnergy)
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
            If Double.IsNaN(_MetalCapacity) OrElse Double.IsNaN(_MetalProduction) OrElse Double.IsNaN(_MetalCapacity) Then
                MetalStoragePercentage = "n/a"
            Else
                MetalStoragePercentage = CInt(CurrentMetal / _MetalCapacity / 10)
            End If
        End Get
    End Property

    Public ReadOnly Property CrystalStoragePercentage() As String
        Get
            If Double.IsNaN(_CrystalCapacity) OrElse Double.IsNaN(_CrystalProduction) OrElse Double.IsNaN(_CrystalCapacity) Then
                CrystalStoragePercentage = "n/a"
            Else
                CrystalStoragePercentage = CInt(CurrentCrystal / _CrystalCapacity / 10)
            End If
        End Get
    End Property

    Public ReadOnly Property DeuteriumStoragePercentage() As String
        Get
            If Double.IsNaN(_DeuteriumCapacity) OrElse Double.IsNaN(_DeuteriumProduction) OrElse Double.IsNaN(_DeuteriumCapacity) Then
                DeuteriumStoragePercentage = "n/a"
            Else
                DeuteriumStoragePercentage = CInt(CurrentDeuterium / _DeuteriumCapacity / 10)
            End If
        End Get
    End Property

    Public ReadOnly Property MetalOverflowETA() As String
        Get
            Return EstimateTime(_Planet.Metal, _MetalProduction, _MetalCapacity)
        End Get
    End Property

    Public ReadOnly Property CrystalOverflowETA() As String
        Get
            Return EstimateTime(_Planet.Crystal, _CrystalProduction, _CrystalCapacity)
        End Get
    End Property

    Public ReadOnly Property DeuteriumOverflowETA() As String
        Get
            Return EstimateTime(_Planet.Deuterium, _DeuteriumProduction, _DeuteriumCapacity)
        End Get
    End Property

    Public ReadOnly Property Total() As String
        Get
            If Double.IsNaN(_MetalProduction) OrElse Double.IsNaN(_CrystalProduction) OrElse Double.IsNaN(_DeuteriumProduction) OrElse _
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
            If Double.IsNaN(_MetalProduction) OrElse Double.IsNaN(_CrystalProduction) OrElse Double.IsNaN(_DeuteriumProduction) Then
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

        Dim level As Integer
        Dim percentage As Integer
        Dim _BuildingLevelMap As Dictionary(Of Gid, Integer) = _Planet.BuildingLevelMap
        Dim _productionMap As Dictionary(Of Gid, Integer) = _Planet.ProductionMap
        Dim _HighestTemperature As Integer = _Planet.HighestTemperature
        Dim _StationaryFleetMap As Dictionary(Of Gid, Integer) = _Planet.StationaryFleetMap

        If _BuildingLevelMap Is Nothing Then
            Exit Sub
        End If

        'begin: calculate capacities
        'Dim level As Integer

        'metal
        If _BuildingLevelMap.ContainsKey(Gid.MetalStorage) Then
            level = _BuildingLevelMap(Gid.MetalStorage)
        Else
            level = 0
        End If
        _MetalCapacity = CalculateCapacity(level)

        'crystal
        If _BuildingLevelMap.ContainsKey(Gid.CrystalStorage) Then
            level = _BuildingLevelMap(Gid.CrystalStorage)
        Else
            level = 0
        End If
        _CrystalCapacity = CalculateCapacity(level)

        'deuterium
        If _BuildingLevelMap.ContainsKey(Gid.DeuteriumTank) Then
            level = _BuildingLevelMap(Gid.DeuteriumTank)
        Else
            level = 0
        End If
        _DeuteriumCapacity = CalculateCapacity(level)

        'end: calculate capacities

        If _productionMap Is Nothing Then
            Exit Sub
        End If

        'begin: calculate production and power consumption

        _PowerConsumption = 0

        'metal mine
        If _BuildingLevelMap.ContainsKey(Gid.MetalMine) AndAlso _productionMap.ContainsKey(Gid.MetalMine) Then
            level = _BuildingLevelMap(Gid.MetalMine)
            percentage = _productionMap(Gid.MetalMine)
            _MetalProduction = 20 + 30 * level * 1.1 ^ level * percentage / 100
            _MetalMineEnergy = -10 * level * 1.1 ^ level * percentage / 100
            _PowerConsumption += -_MetalMineEnergy
            _MetalMineLevel = level
            _MetalMineProductionPercentage = percentage
        Else
            _MetalProduction = 20
            _MetalMineEnergy = 0
            _MetalMineLevel = 0
            _MetalMineProductionPercentage = 0
        End If

        'crystal mine
        If _BuildingLevelMap.ContainsKey(Gid.CrystalMine) AndAlso _productionMap.ContainsKey(Gid.CrystalMine) Then
            level = _BuildingLevelMap(Gid.CrystalMine)
            percentage = _productionMap(Gid.CrystalMine)
            _CrystalProduction = 10 + 20 * level * 1.1 ^ level * percentage / 100
            _CrystalMineEnergy = -10 * level * 1.1 ^ level * percentage / 100
            _PowerConsumption += -_CrystalMineEnergy
            _CrystalMineLevel = level
            _CrystalMineProductionPercentage = percentage
        Else
            _CrystalProduction = 10
            _CrystalMineEnergy = 0
            _CrystalMineLevel = 0
            _CrystalMineProductionPercentage = 0
        End If

        'deuterium synthesizer
        If _BuildingLevelMap.ContainsKey(Gid.DeuteriumSynthesizer) AndAlso _productionMap.ContainsKey(Gid.DeuteriumSynthesizer) Then
            level = _BuildingLevelMap(Gid.DeuteriumSynthesizer)
            percentage = _productionMap(Gid.DeuteriumSynthesizer)
            _DeuteriumProduction = 10 * level * 1.1 ^ level * (-0.002 * _HighestTemperature + 1.28) * percentage / 100
            _DeuteriumSynthesizerEnergy = -20 * level * 1.1 ^ level * percentage / 100
            _PowerConsumption += -_DeuteriumSynthesizerEnergy
            _DeuteriumSynthesizerLevel = level
            _DeuteriumSynthesizerProductionPercentage = percentage
        Else
            _DeuteriumProduction = 0
            _DeuteriumSynthesizerEnergy = 0
            _DeuteriumSynthesizerLevel = 0
            _DeuteriumSynthesizerProductionPercentage = 0
        End If

        'end: calculate production and power consumption

        'begin: calculate power generation and deuterium consumption

        _PowerGeneration = 0

        'solar planet
        If _BuildingLevelMap.ContainsKey(Gid.SolarPlant) AndAlso _productionMap.ContainsKey(Gid.SolarPlant) Then
            level = _BuildingLevelMap(Gid.SolarPlant)
            percentage = _productionMap(Gid.SolarPlant)
            _SolarPlanetEnergy = 20 * level * 1.1 ^ level * percentage / 100
            _PowerGeneration += _SolarPlanetEnergy
            _SolarPlanetLevel = level
            _SolarPlanetProductionPercentage = percentage
        Else
            _SolarPlanetEnergy = 0
            _SolarPlanetLevel = 0
            _SolarPlanetProductionPercentage = 0
        End If

        'fusion planet (_DeuteriumProduction is valid)
        If _BuildingLevelMap.ContainsKey(Gid.FusionReactor) AndAlso _productionMap.ContainsKey(Gid.FusionReactor) Then
            level = _BuildingLevelMap(Gid.FusionReactor)
            percentage = _productionMap(Gid.FusionReactor)
            _FusionReactorEnergy = 50 * level * 1.1 ^ level * percentage / 100
            _PowerGeneration += _FusionReactorEnergy
            '_DeuteriumProduction -= 10 * level * 1.1 ^ level * (-0.002 * _HighestTemperature + 1.28) * percentage / 100
            _DeuteriumConsumption = 10 * level * 1.1 ^ level * (-0.002 * _HighestTemperature + 1.28) * percentage / 100
            _FusionReactorLevel = level
            _FusionReactorProductionPercentage = percentage
        Else
            _FusionReactorEnergy = 0
            _DeuteriumConsumption = 0
            _FusionReactorLevel = 0
            _FusionReactorProductionPercentage = 0
        End If

        If _StationaryFleetMap Is Nothing Then
            Exit Sub
        End If

        'solar satelites
        If _StationaryFleetMap.ContainsKey(Gid.SolarSatellite) AndAlso _productionMap.ContainsKey(Gid.SolarSatellite) Then
            level = _StationaryFleetMap(Gid.SolarSatellite)
            percentage = _productionMap(Gid.SolarSatellite)
            _SolarSateliteEnergy = Math.Min(_HighestTemperature / 4 + 20, 50) * level * percentage / 100
            _PowerGeneration += _SolarSateliteEnergy
            _SolarSateliteLevel = level
            _SolarSateliteProductionPercentage = percentage
        Else
            _SolarSateliteEnergy = 0
            _SolarSateliteLevel = 0
            _SolarSateliteProductionPercentage = 0
        End If

        'end: calculate power generation and deuterium consumption

        'adjust consumption and production
        If _PowerConsumption > _PowerGeneration Then
            Dim k As Double = _PowerGeneration / _PowerConsumption
            _MetalProduction = (_MetalProduction - 20) * k + 20
            _CrystalProduction = (_CrystalProduction - 10) * k + 10
            _DeuteriumProduction = _DeuteriumProduction * k - _DeuteriumConsumption

            _MetalMineEnergy = _MetalMineEnergy * k
            _CrystalMineEnergy = _CrystalMineEnergy * k
            _DeuteriumSynthesizerEnergy = _DeuteriumSynthesizerEnergy * k
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

        Return _MetalProduction + _CrystalProduction + _DeuteriumProduction

    End Function

    Private Function EstimateTime(ByVal stock As Integer, ByVal growth As Double, ByVal capacity As Double) As String

        If Double.IsNaN(growth) OrElse Double.IsNaN(capacity) Then
            EstimateTime = "n/a"
        ElseIf growth <= 0 Then
            EstimateTime = "never"
        Else
            Dim w As Single = (capacity - stock) / growth / 24 / 7
            Dim wi As Integer = Floor(w)
            If wi > 0 Then
                Return "more than " & wi & " week(s)"
            End If

            Dim d As Single = (w - wi) * 7
            Dim di As Integer = Floor(d)
            If di > 0 Then
                Return "more than " & di & " day(s)"
            End If

            Dim h As Single = (d - di) * 24
            Dim hi As Integer = Floor(h)
            Dim m As Single = (h - hi) * 60
            Dim mi As Integer = Floor(m)
            Dim s As Single = (m - mi) * 60
            Dim si As Integer = Floor(s)

            EstimateTime = hi & "h " & mi & "m " & si & "s"
        End If
    End Function

    Private ReadOnly Property CurrentMetal() As Integer
        Get
            Return Min(_Planet.Metal + _MetalProduction * (Now - _Planet.LocalTime).TotalHours, _MetalCapacity * 1000)
        End Get
    End Property

    Private ReadOnly Property CurrentCrystal() As Integer
        Get
            Return Min(_Planet.Crystal + _CrystalProduction * (Now - _Planet.LocalTime).TotalHours, _CrystalCapacity * 1000)
        End Get
    End Property

    Private ReadOnly Property CurrentDeuterium() As Integer
        Get
            Return Min(Max(_Planet.Deuterium + _DeuteriumProduction * (Now - _Planet.LocalTime).TotalHours, 0), _DeuteriumCapacity * 1000)
        End Get
    End Property
End Class
