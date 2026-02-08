using System;
using UnityEngine;

namespace Project.ThirdParty.ScratchCard.Scripts.Core.InputData
{
	public class ScratchCardInput
	{
		#region Events

		public event ScratchHandler OnScratch;
		public event Action<Vector2, float> OnScratchHole;
		public event Action<ScratchCardInputData> OnScratchHoleExtended;
		public event Action<Vector2, float, Vector2, float> OnScratchLine;
		public event Action<ScratchCardInputData, ScratchCardInputData> OnScratchLineExtended;
		public delegate Vector2 ScratchHandler(Vector2 position);
		
		#endregion

		public bool CheckCanvasRaycasts;
		public bool UsePressure;

        public bool GetIsScratching()
        {
            if (isScratching != null)
            {
                foreach (bool scratching in isScratching)
                {
                    if (scratching)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private readonly Func<bool> isScratched;
		private RaycastController raycastController;
		private ScratchCardInputData[] startInputData;
		private ScratchCardInputData[] endInputData;
		private Vector2?[] previousScratchPosition;
		private Vector2 scratchPosition;
		private bool[] isScratching;
		private bool[] isStartPosition;

		private const int MaxTouchCount = 10;
		private const int ReserveTouchCount = 1;

		public ScratchCardInput(Func<bool> scratched)
		{
			isScratched = scratched;
			isScratching = new bool[MaxTouchCount + ReserveTouchCount];
			isStartPosition = new bool[MaxTouchCount + ReserveTouchCount];
			startInputData = new ScratchCardInputData[MaxTouchCount + ReserveTouchCount];
			endInputData = new ScratchCardInputData[MaxTouchCount + ReserveTouchCount];
			previousScratchPosition = new Vector2?[MaxTouchCount + ReserveTouchCount];
			for (int i = 0; i < isStartPosition.Length; i++)
			{
				isStartPosition[i] = true;
			}
		}
		
		public void InitRaycastsController(GameObject surfaceObject, Canvas[] canvases)
		{
			raycastController = new RaycastController(surfaceObject, canvases);
		}

		public bool TryUpdate()
		{
            if (Input.touchSupported && Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    int fingerId = touch.fingerId;
                    if (fingerId >= MaxTouchCount)
                        continue;

                    if (touch.phase == TouchPhase.Began)
                    {
                        isScratching[fingerId] = false;
                        isStartPosition[fingerId] = true;
                    }

                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        SetInputData(fingerId, touch.position);
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        isScratching[fingerId] = false;
                        previousScratchPosition[fingerId] = null;
                    }

                    Scratch();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isScratching[0] = false;
                    isStartPosition[0] = true;
                }

                if (Input.GetMouseButton(0))
                {
                    SetInputData(0, Input.mousePosition);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    isScratching[0] = false;
                    previousScratchPosition[0] = null;
                }

                Scratch();
            }	return GetIsScratching();
		}

		private void SetInputData(int fingerId, Vector2 position, float pressure = 1f)
		{
            if (!isScratching[fingerId] && CheckCanvasRaycasts && raycastController != null && raycastController.IsBlock(position))
            {
                return;
            }

			if (OnScratch != null)
			{
				scratchPosition = OnScratch(position);
			}

			if (isStartPosition[fingerId])
			{
				startInputData[fingerId].Position = scratchPosition;
				startInputData[fingerId].Pressure = pressure;
				startInputData[fingerId].Time = Time.time;
				endInputData[fingerId] = startInputData[fingerId];
				isStartPosition[fingerId] = !isStartPosition[fingerId];
			}
			else
			{
				startInputData[fingerId] = endInputData[fingerId];
				endInputData[fingerId].Position = scratchPosition;
				endInputData[fingerId].Pressure = pressure;
				endInputData[fingerId].Time = Time.time;
			}

			if (!isScratching[fingerId])
			{
				endInputData[fingerId] = startInputData[fingerId];
				isScratching[fingerId] = true;
			}
		}

		public void Scratch()
		{
			for (int i = 0; i < isScratching.Length; i++)
			{
				if (isScratching[i])
				{
					if (startInputData[i].Position == endInputData[i].Position)
					{
						OnScratchHole?.Invoke(endInputData[i].Position, endInputData[i].Pressure);
						if (isScratched != null && isScratched())
						{
							OnScratchHoleExtended?.Invoke(endInputData[i]);
						}
					}
					else
					{
						OnScratchLine?.Invoke(startInputData[i].Position, startInputData[i].Pressure,
							endInputData[i].Position, endInputData[i].Pressure);
						if (isScratched != null && isScratched())
						{
							OnScratchLineExtended?.Invoke(startInputData[i], endInputData[i]);
						}
					}
				}
			}
		}

		public void Scratch(Vector2 screenPosition)
		{
			SetInputData(MaxTouchCount, screenPosition);
		}

		public void ResetData()
		{
			for (int i = 0; i < isScratching.Length; i++)
			{
				isScratching[i] = false;
				isStartPosition[i] = true;
				previousScratchPosition[i] = null;
			}
		}
	}
}