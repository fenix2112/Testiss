using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System.Collections;

namespace Game {
    public class SlotView : View
    {
        public GameObject Game;

        public Signal<List<ItemViewVO>> UserSetShapeToGridSignal;
        public Signal CheckAndRemoveItemsSignal;
        public Signal UserCompletedMoveSignal;

        public bool IsEmpty = true;
        private bool isMoving = false;
        private bool IsDragable = true;

        private Vector3 offset;
        private Vector3 screenPoint;
        private float size = 0;
        private List<GameObject> Temp;


        public GameObject Shape { get; set; }

        override protected void Awake()
        {
            base.Awake();
            UserSetShapeToGridSignal = new Signal<List<ItemViewVO>>();
            CheckAndRemoveItemsSignal = new Signal();
            Temp = new List<GameObject>();
        }

        override protected void Start()
        {
            base.Start();
        }

        public void UpdateView(GameObject obj)
        {
            Shape = Instantiate(obj, new Vector3(this.transform.position.x, this.transform.position.y, 0.0f), Quaternion.identity);
            Shape.transform.parent = this.transform;
            Shape.transform.localScale = new Vector3(0.5f, 0.5f);

            Vector2 pos = new Vector2();
            pos.x = this.transform.position.x - Shape.transform.lossyScale.x / 2;
            pos.y = this.transform.position.y;
            Shape.transform.position = pos;


            screenPoint =  Shape.transform.position;

            IsEmpty = false;
        }

        void OnMouseDown()
        {
            if (!isMoving && !IsEmpty)
            {
                Shape.transform.localScale = new Vector3(0.75f, 0.75f);
                isMoving = true;
                IsDragable = true;
                offset = Shape.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }
        }

        void OnMouseDrag()
        {
            if (IsDragable)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                Shape.transform.position = curPosition;
            }
        }

        void OnMouseUp()
        {
            if (isMoving && !IsEmpty)
            {
                isMoving = false;
                IsEmpty = true;
                IsDragable = false;
                List<ItemViewVO> ListItemView = new List<ItemViewVO>();

                while (Shape.transform.childCount > 0)
                {
                    Transform child = Shape.transform.GetChild(0);
                    child.transform.parent = Game.transform;

                    if (size == 0)
                    {
                        size = child.GetComponent<Renderer>().bounds.size.x;
                    }

                    Vector3 pos = child.transform.localPosition;
                    pos.x = Mathf.Floor(pos.x / size) * size; 
                    pos.y = Mathf.Floor(pos.y / size) * size;
                    Vector2 Pos = new Vector2(pos.x, pos.y);

                    GameObject item = child.gameObject;

                    ItemViewVO shapeItemView = item.GetComponent<ItemViewVO>();
                    if (shapeItemView)
                    {
                        shapeItemView.OldPos = child.transform.localPosition;
                        shapeItemView.Pos = Pos;
                        shapeItemView.Size = size;
                        ListItemView.Add(shapeItemView);
                        Temp.Add(item);
                    }
                }
            
               UserSetShapeToGridSignal.Dispatch(ListItemView);
            }
        }

        public void SetShapeToPos() {
            IsDragable = false;
            IsEmpty = true;
            isMoving = false;
           for (int i = 0; i < Temp.Count; i++)
            {
                GameObject item = Temp[i] as GameObject;
                ItemViewVO shapeItemView = item.GetComponent<ItemViewVO>();
                item.transform.localPosition = shapeItemView.Pos;
            }
            CheckAndRemoveItemsSignal.Dispatch();
            Temp.Clear();
            Destroy(Shape);
        }

        public void SetShapeToOrigin()
        {
            IsDragable = true;
            IsEmpty = false;
            isMoving = false;

            for (int i = 0; i < Temp.Count; i++)
            {
                GameObject item = Temp[i] as GameObject;
                ItemViewVO shapeItemView = item.GetComponent<ItemViewVO>();
                item.transform.localPosition = shapeItemView.OldPos;
                item.transform.parent = Shape.transform;

            }

            Temp.Clear();
            Shape.transform.position = screenPoint;
            Shape.transform.parent = this.transform;
            Shape.transform.localScale = new Vector3(0.5f, 0.5f);
        }

        public void RemoveLines(List<List<GameObject>> list)
        {
            while (list.Count > 0)
            {
                List<GameObject> items = list[0];
                float delta = 0.5f;
                while (items.Count > 0)
                {
                    GameObject item = items[items.Count - 1] as GameObject;
                    Rigidbody2D rigid = item.GetComponent<Rigidbody2D>();
                    SpriteRenderer renderer = item.GetComponent<SpriteRenderer>();
                    delta -= 0.05f;
                    StartCoroutine(Example(rigid , renderer, delta));
                
                    items.Remove(item);
                }
                list.Remove(items);
                items.Clear();
            }
        }

        IEnumerator Example(Rigidbody2D rigid, SpriteRenderer renderer, float delta)
        {
            yield return new WaitForSeconds(delta);
            rigid.simulated = true;
            rigid.gravityScale = 1.9f;
            renderer.material.renderQueue =-3000;
        }


     
    }



}

