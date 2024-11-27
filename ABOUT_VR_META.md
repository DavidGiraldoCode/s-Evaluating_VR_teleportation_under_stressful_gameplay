# VR in Meta

## Modifying the teleportation.

Important to notice that the TeleportActiveState is handled via a component called `ActiveStateGate` that is attached to the Prefab with the same name (TeleportActiveState). This gate has an `Open` and `Close` Selector, which are the most important parts since they actually enable the Selector to start selecting (who would it thought? 🙃). The logic in the `Open` Selector is going to show the `TeleportArc,` and the logic inside the Selector (after being enabled) is what is going to trigger the actual teleportation.

## Controlls inputs and how to get data

<img width="800" alt="image" src="/Assets/Art/Images/controlls_inputs.png"></br>

```C#
public class YourCustomComponent : MonoBehaviour
{
    public ControllerRef _rightControllerRef; // ControllerRef is the carrier of the IController interface that allows to the in input data
    public ControllerRef _leftControllerRef;

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
```

## How to subscribe to selected states when teleporting
```C#
ActiveStateSelector _openSelector;
_openSelector.WhenSelected += OnOpenSelected; // Remember to unsubscribe

```

## How to check change orientation in teleportation -> WIP
- The teleportation in the left hand is disabled

## How to forcibly disable an ongoing grabbing interaction to reset a grabbed object's default position.

```C#
GameObject _ISDK_HandGrabInteraction; // Get a reference to the ISDK_HandGrabInteraction GameObject

void ResetBuzzWirePosition()
{
    _ISDK_HandGrabInteraction.SetActive(false); // Disable the grabbing by force
    gameObject.transform.position = _defaultLocation.position;
    gameObject.transform.rotation = _defaultLocation.rotation;
    _ISDK_HandGrabInteraction.SetActive(true); // Enable again
}
```

## How to subscribe to (Snap)Interactables events

```C#
// Reference the Interactable
private SnapInteractable m_snapInteractable;
m_snapInteractable = GetComponent<SnapInteractable>();

// Subscribe to the delegate (recall unsubscribing)
m_snapInteractable.WhenStateChanged += OnSnapChange;

// Implement the method
private void OnSnapChange(InteractableStateChangeArgs args)
{
    Debug.Log(args.NewState);
     /*yout code*/
}
```

## How the teleportation Nav area is made?
I used normal cylinders with the size of the platform, and include then in the baking os the Nav area.
```bash
NavGroup
| - GameObject - NavMesh Surface
| - ISDK_TeleportInteraction
| - PlatformRef(n)
```

### How the [interaction with the world (poke, grab, snap) works](https://developers.meta.com/horizon/documentation/unity/unity-isdk-architectural-overview/)

### Getting [Data from controller](https://developers.meta.com/horizon/documentation/unity/unity-isdk-input-processing#controller)

`IController`: Main way to get data from the controller

### Teleport Interaction [🔗](https://developers.meta.com/horizon/documentation/unity/unity-isdk-teleport-interaction#teleport-interactor)

### Active State [🔗](https://developers.meta.com/horizon/unity/unity-isdk-use-active-state/?doc_root=documentation)

### Interactable Lifecycle [🔗](https://developers.meta.com/horizon/documentation/unity/unity-isdk-interactor-interactable-lifecycle/)

### Interactor [🔗](https://developers.meta.com/horizon/documentation/unity/unity-isdk-interactor)

### The [Grabbable components 🔗](https://developers.meta.com/horizon/documentation/unity/unity-isdk-grabbable/) and How to [Create grab interactions 🔗](https://developers.meta.com/horizon/documentation/unity/unity-isdk-create-hand-grab-interactions-legacy/)

### Use [quick actions 🔗](https://developers.meta.com/horizon/documentation/unity/unity-isdk-quick-actions/) to set the default grabbable on any GameObject and the controllers

## How to do [Snapping interactions 🔗](https://developers.meta.com/horizon/documentation/unity/unity-isdk-snap-interaction)
