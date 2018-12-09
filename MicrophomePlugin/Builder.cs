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
        /// <param name="capsuleDiametr">диаметр капсюли</param>
        /// <param name="clipLenght">длина зажима</param>
        /// <param name="handleDiametr">диаметр ручки</param>
        /// <param name="handleLenght">длина ручки</param>
        /// <param name="totalLenght">общая длина микрофона</param>
        public Builder(double capsuleDiametr, double clipLenght, double handleDiametr, double handleLenght, double totalLenght)
        {
            using (var connector = new CadConnect())
            {
                var document = connector.Connect();
                document.Create(false, true);
                document = (ksDocument3D)connector.Kompas.ActiveDocument3D();
                var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
                CreateBase(document, capsuleDiametr / 2, clipLenght, handleDiametr /2, handleLenght, totalLenght);
                PullBase(document);
                connector.Kompas.Visible = true;
            }
        }

        private void PullBase(ksDocument3D document)
        {
        }

        private void CreateBase(ksDocument3D document, double capsuleRadius, double clipLenght, double handleRadius, double handleLenght, double totalLenght)
        {
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            var currentPlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeYOZ);
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
            var capsuleStartX = totalLenght - capsuleRadius * 2;
            _sketchEdit.ksLineSeg(handleLenght, handleRadius, capsuleStartX, capsuleRadius, 1);
            _sketchEdit.ksLineSeg(capsuleStartX, capsuleRadius, capsuleStartX + clipLenght, capsuleRadius, 1);
            _sketchEdit.ksArcByPoint(capsuleStartX + clipLenght, 0, capsuleRadius,
                 totalLenght, 0, 
                 capsuleStartX + clipLenght, capsuleRadius,
                 1, 1);

            _sketchEdit.ksLineSeg
                (-4, 0, totalLenght + 4, 0, 3);
            _sketchDefinition.EndEdit();

            var entityRotated =
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseRotated);
            var entityRotatedDefinition =
                (ksBaseRotatedDefinition)entityRotated.GetDefinition();

            entityRotatedDefinition.directionType = 0;
            entityRotatedDefinition.SetSideParam(true, 360);
            entityRotatedDefinition.SetSketch(_entitySketch);
            entityRotated.Create();
        }


        /// <summary>
        /// очистка данных
        /// </summary>
        public void Dispose()
        {
        }
    }
}
