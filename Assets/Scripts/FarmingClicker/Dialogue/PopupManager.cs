namespace FarmingClicker.Dialogue
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using DialoguePanelControllers;
    using Sirenix.OdinInspector;
    using UnityEngine;
    
    public class PopupManager : SerializedMonoBehaviour, IMessageReceiver
    {
        //[field: SerializeField, FoldoutGroup("UI")]  private DialoguePopupPanel dialoguePopupPanel;
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        private void Start()
        {
            //ListenedTypes.Add(typeof(DisplayDialogueCommand));

            MessageDispatcher.Instance.RegisterReceiver(this);
        }

        void ClosePopup(PopupPanelBase panelBase)
        {
            Debug.Log($"Closing popup {panelBase}");
            panelBase.OnFinished -= ClosePopup;
            panelBase.gameObject.SetActive(false);
        }
        
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                /*case DisplayDialogueCommand command:
                {
                    var currentConversation = database.conversations.FirstOrDefault(c => c.id == command.ID);
                    if(currentConversation == null)
                    {
                        throw new
                            Exception($"Conversation with id {command.ID} not found in the database");
                    }

                    List<Sprite> sprites = new List<Sprite>();
                    foreach(var dialogueDatabaseEntry in currentConversation.dialogues)
                    {
                        sprites.Add(database.GetMappedEmotion(dialogueDatabaseEntry.character,
                                                  dialogueDatabaseEntry.emotion));
                    }

                    var data = new DialoguePopupData(currentConversation, sprites);
                    dialoguePopupPanel.gameObject.SetActive(true);
                    dialoguePopupPanel.OnFinished += ClosePopup;
                    dialoguePopupPanel.SetupData(data);
                    
                    break;
                }*/
                
            }

        }
    }
}
