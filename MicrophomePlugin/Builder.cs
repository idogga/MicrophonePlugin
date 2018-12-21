using Kompas6API5;
using Kompas6Constants3D;
using System;

namespace MicrophonePlugin
{
    /// <summary>
    /// Строитель модели
    /// </summary>
    public class Builder : IDisposable
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parameters">Параметры</param>
        public Builder(Parameters parameters)
        {
            using (var connector = new CadConnect())
            {
                var document = connector.Connect();
                document.Create(false, true);
                document = (ksDocument3D)connector.Kompas.ActiveDocument3D();
                var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
                CreateBase(connector, document, parameters.CapsuleRadius / 2, 
                    parameters.ClipLenght, parameters.HandleDiametr / 2,
                    parameters.HandleLenght,
                    parameters.TotalLenght,
                    parameters.GridLenght);
                connector.Kompas.Visible = true;
            }
        }

        private void CreateBase(CadConnect connector, ksDocument3D document, double capsuleRadius, double clipLenght, double handleRadius, double handleLenght, double totalLenght, double gridLenght)
        {
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            var currentPlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            var _entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            var _sketchDefinition = (ksSketchDefinition)_entitySketch.GetDefinition();
            _sketchDefinition.SetPlane(currentPlane);
            _entitySketch.name = "1";
            _entitySketch.Create();
            var _sketchEdit = (ksDocument2D)_sketchDefinition.BeginEdit();
            _sketchEdit.ksLineSeg
                (0, 0, 0, handleRadius, 1);
            _sketchEdit.ksLineSeg
                (0, handleRadius, handleLenght, handleRadius, 1);
            var capsuleStartX = totalLenght - capsuleRadius - clipLenght;
            _sketchEdit.ksLineSeg(handleLenght, handleRadius, capsuleStartX, capsuleRadius, 1);
            _sketchEdit.ksLineSeg(capsuleStartX, capsuleRadius, totalLenght - capsuleRadius, capsuleRadius, 1);
            _sketchEdit.ksArcByPoint(totalLenght - capsuleRadius, 0, 
                capsuleRadius,
                 totalLenght - capsuleRadius, capsuleRadius, 
                 totalLenght, 0,
                 -1, 1);
            _sketchEdit.ksLineSeg(totalLenght, 0, 0, 0, 1);
            _sketchEdit.ksLineSeg
                (0, 0, totalLenght, 0, 3);

            
            _sketchDefinition.EndEdit();
            var entityRotated =
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseRotated);
            var entityRotatedDefinition =
                (ksBaseRotatedDefinition)entityRotated.GetDefinition();

            entityRotatedDefinition.directionType = 0;
            entityRotatedDefinition.SetSideParam(true, 360);
            entityRotatedDefinition.SetSketch(_entitySketch);
            entityRotated.Create();
            document.shadedWireframe = true;
            document.drawMode = 3;

            CreateHorizontalTors(part, totalLenght, gridLenght, capsuleRadius);
            CreateVerticalTorsRight(part, totalLenght, gridLenght, capsuleRadius);
            
                CreateVerticalTorsLeft(part, totalLenght, gridLenght, capsuleRadius);
        }

        private void CreateHorizontalTors(ksPart part, double totalLenght, double gridLenght, double capsuleRadius)
        {
            var start = totalLenght - capsuleRadius;
            
            while (start < totalLenght)
            {
                var currentPlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
                var _entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                var _sketchDefinition = (ksSketchDefinition)_entitySketch.GetDefinition();
                _sketchDefinition.SetPlane(currentPlane);
                _entitySketch.name = "Horizontal : " /*+ start.ToString()*/;
                _entitySketch.Create();
                var _sketchEdit = (ksDocument2D)_sketchDefinition.BeginEdit();
                var entityRotated =
                        (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseRotated);
                var entityRotatedDefinition =
                    (ksBaseRotatedDefinition)entityRotated.GetDefinition();
                start += 2 * gridLenght;
                _sketchEdit.ksCircle(start, Math.Sqrt(Math.Pow(capsuleRadius, 2) - Math.Pow(start - totalLenght + capsuleRadius, 2)), gridLenght / 2, 1);

                _sketchEdit.ksLineSeg(0, 0, totalLenght, 0, 3);
                _sketchDefinition.EndEdit();           

                entityRotatedDefinition.directionType = 0;
                entityRotatedDefinition.SetSideParam(true, 360);
                entityRotatedDefinition.SetSketch(_entitySketch);
            entityRotated.Create();
            }

        }

        private void CreateVerticalTorsLeft(ksPart part, double totalLenght, double gridLenght, double capsuleRadius)
        {
            var start = totalLenght;
            while (start > totalLenght - capsuleRadius)
            {
                start -= 2 * gridLenght;

                var currentPlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                var _entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                var _sketchDefinition = (ksSketchDefinition)_entitySketch.GetDefinition();
                _sketchDefinition.SetPlane(currentPlane);
                _entitySketch.name = "Vertical : " + start.ToString();
                _entitySketch.Create();
                var _sketchEdit = (ksDocument2D)_sketchDefinition.BeginEdit();
                _sketchEdit.ksCircle(start, - Math.Sqrt(Math.Pow(capsuleRadius, 2) - Math.Pow(start - totalLenght + capsuleRadius, 2)), gridLenght / 2, 1);
                _sketchEdit.ksLineSeg(totalLenght - capsuleRadius, -capsuleRadius, totalLenght - capsuleRadius, capsuleRadius, 3);
                _sketchDefinition.EndEdit();
                var entityRotated =
                    (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseRotated);
                var entityRotatedDefinition =
                    (ksBaseRotatedDefinition)entityRotated.GetDefinition();

                entityRotatedDefinition.directionType = 3;
                entityRotatedDefinition.SetSideParam(true, 180);
                entityRotatedDefinition.SetSketch(_entitySketch);
                entityRotated.Create();
            }
        }

        private void CreateVerticalTorsRight(ksPart part, double totalLenght, double gridLenght, double capsuleRadius)
        {
            var start = totalLenght - capsuleRadius;
            while (start < totalLenght)
            {
                start += 2 * gridLenght;
                var currentPlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                var _entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                var _sketchDefinition = (ksSketchDefinition)_entitySketch.GetDefinition();
                _sketchDefinition.SetPlane(currentPlane);
                _entitySketch.name = "Vertical : " + start.ToString();
                _entitySketch.Create();
                var _sketchEdit = (ksDocument2D)_sketchDefinition.BeginEdit();

                var entityRotated =
                    (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseRotated);
                var entityRotatedDefinition =
                    (ksBaseRotatedDefinition)entityRotated.GetDefinition();
                _sketchEdit.ksCircle(start, Math.Sqrt(Math.Pow(capsuleRadius, 2) - Math.Pow(start - totalLenght + capsuleRadius, 2)), gridLenght / 2, 1);

                _sketchEdit.ksLineSeg(totalLenght - capsuleRadius, -capsuleRadius, totalLenght - capsuleRadius, capsuleRadius, 3);
                _sketchDefinition.EndEdit();
                entityRotatedDefinition.directionType = 3;
                entityRotatedDefinition.SetSideParam(true, 180);
                entityRotatedDefinition.SetSketch(_entitySketch);
                entityRotated.Create();
            }
        }

        /// <summary>
        /// очистка данных
        /// </summary>
        public void Dispose()
        {
        }
    }
}
