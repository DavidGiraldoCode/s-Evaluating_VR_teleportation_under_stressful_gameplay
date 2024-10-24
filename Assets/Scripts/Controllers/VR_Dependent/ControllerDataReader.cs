using UnityEngine;
using Oculus.Interaction.Input;
using Oculus.Interaction.PoseDetection.Debug;
using Oculus.Interaction.Locomotion;
using Oculus.Interaction;
using System;
using Oculus.Interaction.Surfaces;
using Oculus.Interaction.DistanceReticles;
using UnityEditor.PackageManager;

/// <summary>
/// This is a debugging class to read data form the controller
/// </summary>
public class ControllerDataReader : MonoBehaviour
{
    public ControllerRef _rightControllerRef;
    public ControllerRef _leftControllerRef;
    public TeleportInteractable _teleportInteractable;
    public TeleportInteractor _teleportInteractor;
    public Transform _gameObjTransform;
    private Transform _gameObjTransformPrevious;
    public TeleportArcGravity _teleportArcGravity;
    public ActiveStateSelector _activeStateSelector;
    public ActiveStateSelector _openSelector;

    public Transform _arcVisualTarget;
    private bool _preTeleportBunny = false;

    private Vector3 from, to;

    private void Awake()
    {
        _activeStateSelector.WhenSelected += OnSelected;
        _activeStateSelector.WhenUnselected += OnUnselected;

        _openSelector.WhenSelected += OnOpenSelected;
        _openSelector.WhenUnselected += OnOpenUnselected;

        _gameObjTransformPrevious = _gameObjTransform;
    }

    private void OnOpenUnselected()
    {
        Debug.Log("XXX OnOpenUnselected!");
        _preTeleportBunny = false;
        //Debug.Log(" OnOpenUnselected!");
    }

    private void OnOpenSelected()
    {
        Debug.Log("XXX OnOpenSelected!");
        _preTeleportBunny = true;
        ///Debug.Log("XXX _teleportInteractor.ArcEnd.Point: " + _teleportInteractor.ArcEnd.Point);
        //Debug.Log("XXX _teleportArcGravity.PointAtIndex(n-1) " + _teleportArcGravity.PointAtIndex(_teleportArcGravity.PointsCount - 1));
        _gameObjTransformPrevious = _gameObjTransform;
        //_gameObjTransform.position = _teleportArcGravity.PointAtIndex(_teleportArcGravity.PointsCount - 1);
        //ReticleDataTeleport r = new ReticleDataTeleport();
        //r.ProcessHitPoint(_teleportInteractor.ArcEnd.Point);
    }

    private void OnUnselected()
    {
        if (!_teleportInteractor.HasValidDestination())
        {
            _gameObjTransform = _gameObjTransformPrevious;
        }
    }

    private void OnDisable()
    {
        _activeStateSelector.WhenSelected -= OnSelected;
        _activeStateSelector.WhenUnselected -= OnUnselected;

        _openSelector.WhenSelected -= OnOpenSelected;
        _openSelector.WhenUnselected -= OnOpenUnselected;
    }

    private void OnSelected()
    {
        //Debug.Log("XXX Selecto!!!!");
        //from = _teleportArcGravity.PointAtIndex(0);
        //to = _teleportArcGravity.PointAtIndex(_teleportArcGravity.PointsCount - 1);


        //_teleportInteractable.Surface.ClosestSurfacePoint(to, out SurfaceHit hit);
        //Debug.Log("XXX Hit.Point" + hit.Point);


        //Debug.Log("XXX TeleportInteractor.HasValidDestination(): " + _teleportInteractor.HasValidDestination());
        //Debug.Log("XXX TeleportInteractor.ArcOrigin " + _teleportInteractor.ArcOrigin);
        //Debug.Log("XXX TeleportInteractor.ArcEnd " + _teleportInteractor.ArcEnd);
        //Debug.Log("XXX TeleportInteractor.TeleportTarget " + _teleportInteractor.TeleportTarget);
        if (_teleportInteractor.HasValidDestination())
        {
            _gameObjTransform.position = _teleportInteractor.ArcEnd.Point;
            _teleportInteractable.InjectOptionalTargetPoint(_gameObjTransform);
        }

        // if (_teleportInteractable.DetectHit(from, to, out TeleportHit hit))
        // {
        //     hit.relativeTo = _gameObjTransform;
        //     Debug.Log("XXX Hit in _teleportInteractable");
        // }
        // else
        // {
        //     Debug.Log("XXX No hit");
        // }
    }

    private void Update()
    {
        if (_rightControllerRef.ControllerInput.PrimaryButton)
        {
            if (_leftControllerRef.ControllerInput.Primary2DAxis.magnitude > 0)
            {
                Vector3 joyStickRotation = new Vector3(_leftControllerRef.ControllerInput.Primary2DAxis.x, 0.0f, _leftControllerRef.ControllerInput.Primary2DAxis.y);
                _gameObjTransform.rotation = Quaternion.LookRotation(joyStickRotation, Vector3.up);
            }

        }

        if (_preTeleportBunny)
        {
            _gameObjTransform.position = _arcVisualTarget.position;
        }

        // Debug.Log("XXX: _teleportArcGravity.PointsCount: " + _teleportArcGravity.PointsCount);
        // Debug.Log("XXX: _teleportArcGravity.PointAtIndex(0): " + _teleportArcGravity.PointAtIndex(0));
        // Debug.Log("XXX: _teleportArcGravity.PointAtIndex(n-1): " + _teleportArcGravity.PointAtIndex(_teleportArcGravity.PointsCount - 1));

    }

    private void PrinterOfInput()
    {
        Debug.Log("GripButton:..........:" + _rightControllerRef.ControllerInput.GripButton);
        Debug.Log("TriggerButton:.......:" + _rightControllerRef.ControllerInput.TriggerButton);
        Debug.Log("MenuButton:..........:" + _rightControllerRef.ControllerInput.MenuButton);
        Debug.Log("Thumbrest:...........:" + _rightControllerRef.ControllerInput.Thumbrest);
        Debug.Log("Primary2DAxis:.......:" + _rightControllerRef.ControllerInput.Primary2DAxis);
        Debug.Log("Primary2DAxisClick:..:" + _rightControllerRef.ControllerInput.Primary2DAxisClick);
        Debug.Log("Primary2DAxisTouch:..:" + _rightControllerRef.ControllerInput.Primary2DAxisTouch);
        Debug.Log("PrimaryButton:.......:" + _rightControllerRef.ControllerInput.PrimaryButton);
        Debug.Log("PrimaryTouch:........:" + _rightControllerRef.ControllerInput.PrimaryTouch);
        Debug.Log("SecondaryButton:.....:" + _rightControllerRef.ControllerInput.SecondaryButton);
        Debug.Log("Secondary2DAxis:.....:" + _rightControllerRef.ControllerInput.Secondary2DAxis);
        Debug.Log("SecondaryTouch:......:" + _rightControllerRef.ControllerInput.SecondaryTouch);
    }

}

