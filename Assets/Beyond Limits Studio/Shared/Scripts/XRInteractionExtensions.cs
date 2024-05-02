/*using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

namespace BeyondLimitsStudios
{
    namespace VRInteractables
    {
        public static class XRBaseInteractableExtension
        {
            /// <summary>
            /// Force deselect the selected interactable.
            ///
            /// This is an extension method for <c>XRBaseInteractable</c>.
            /// </summary>
            /// <param name="interactable">Interactable that has been selected by some interactor</param>
            public static void ForceDeselect(this XRBaseInteractable interactable)
            {
                interactable.interactionManager.CancelInteractableSelection(interactable);
                Assert.IsFalse(interactable.isSelected);
            }
        }

        public static class XRBaseInteractorExtension
        {
            /// <summary>
            /// Force deselect any selected interactable for given interactor.
            ///
            /// This is an extension method for <c>XRBaseInteractor</c>.
            /// </summary>
            /// <param name="interactor">Interactor that has some interactable selected</param>
            public static void ForceDeselect(this XRBaseInteractor interactor)
            {
                interactor.interactionManager.CancelInteractorSelection(interactor);
                Assert.IsFalse(interactor.isSelectActive);
            }
        }
    }
}*/