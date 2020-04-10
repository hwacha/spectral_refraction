#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

out VS_OUT {
    vec3 color;
} vs_out;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

// we assume it's a white light.
uniform vec3 lightPosition;
uniform vec3 lightDirection;
uniform sampler2D spectrum;

void main() {
    // assume theres a 30* angle of incedence with the normal
    // so, the sin value of 
    vs_out.color = texture(spectrum, vec2(aColor.x + aColor.y + aColor.z, 0.5f)).rgb;
    // assume it's a yz plane
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}
