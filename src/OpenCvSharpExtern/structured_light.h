#pragma once

// ReSharper disable IdentifierTypo
// ReSharper disable CppInconsistentNaming
// ReSharper disable CppNonInlineFunctionDefinitionInHeaderFile

#include "include_opencv.h"


CVAPI(ExceptionStatus) structured_light_StructuredLightPattern_generate(
    cv::Ptr<cv::structured_light::StructuredLightPattern> *obj,
    cv::_OutputArray *patternImages,
    int *returnValue)
{
    BEGIN_WRAP
    *returnValue = obj->get()->generate(*patternImages) ? 1 : 0;
    END_WRAP
}

CVAPI(ExceptionStatus) structured_light_StructuredLightPattern_decode(
    cv::Ptr<cv::structured_light::StructuredLightPattern> *obj,
    std::vector< std::vector<cv::Mat> > *patternImages, 
    cv::_OutputArray *disparityMap,
    cv::_InputArray *blackImages,
    cv::_InputArray *whiteImages,
    int flags,
    int *returnValue)
{
    BEGIN_WRAP
    *returnValue = obj->get()->decode(*patternImages, *disparityMap, entity(blackImages), entity(whiteImages), flags) ? 1 : 0;
    END_WRAP
}
