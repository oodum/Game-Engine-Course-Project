using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IEraGenerationStrategy
{
    List<RoomType> GenerateEra();
}