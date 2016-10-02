using UnityEngine;
using System.Collections.Generic;

namespace Oisio.Agent
{
    // bag of components and states
    public abstract class Agent : MonoBehaviour, WorldEntity
    {
        [Header("Superclass dependecies")]
        public NavMeshAgent navMeshAgent;

        public List<AgentComponent> components;
        public AgentState currentState;
        // all available states will be inserted in this array
        public AgentState[] registeredState;

        #region WorldEntity implementation
        public Vector3 WorlPos
        {
            get
            {
                return transform.position;
            }
            set
            {
                navMeshAgent.SetDestination(value);
            }
        }
        #endregion

        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void Update()
        {
            FeedState();
            FeedComponents();
        }

        private void FeedState()
        {
            if (currentState != null)
                currentState.FrameFeed();
        }

        private void FeedComponents()
        {
            if (components == null) return;

            foreach (AgentComponent cmp in components)
                cmp.FrameFeed();
        }

        protected virtual void Init()
        {
            components = InitComponents();
            registeredState = InitStates();
        }

        #region Agent component

        protected abstract List<AgentComponent> InitComponents();

        public virtual void AddComponents(AgentComponent newComponent)
        {
            if (!components.Contains(newComponent)) components.Add(newComponent);
        }

        public virtual T RequestComponent<T>() where T : class, AgentComponent
        {
            foreach(AgentComponent cmp in components)
            {
                if (cmp is T) return (T)cmp;
            }

            Debug.LogError("Agent does not have the requested component, this will return null.");
            return null;
        }

        public void RemoveAllComponents()
        {
            for(int i = 0; i < components.Count; i ++)
                components[i] = null;

            components.Clear();
        }

        #endregion

        #region Agent states

        protected abstract AgentState[] InitStates();

        public virtual void ChangeState<T>(EventTrigger initialTrigger = null) where T : class, AgentState
        {
            T state = GetActionState<T>();

            if (state != null) 
            {
                currentState = state;
                currentState.Init(initialTrigger);
            }
            else  Debug.LogWarning("Current state is not mapped, changes will not apply");
        }

        public virtual T GetActionState<T>() where T : class, AgentState
        {
            foreach(AgentState state in registeredState)
            {
                if (state is T) return (T)state;
            }

            return null;
        }

        #endregion
    }
}