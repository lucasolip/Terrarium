#ifndef REMAP_FUNC
#define REMAP_FUNC

float map(float value, float istart, float istop, float ostart, float ostop) {
    return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
}

float2 map(float2 value, float2 istart, float2 istop, float2 ostart, float2 ostop) {
    return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
}

#endif