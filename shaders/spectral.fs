#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec3 Position;

uniform vec3 cameraPos;
uniform samplerCube skybox;
uniform sampler2D spectrum;

void main()
{   
    // Approximation of Cauchy's Equation
    // n(λ) = B + (C / λ^2)
    // (Hard Crown Glass K5) B = 1.5046, C = 0.00420
    // (Dense flint glass SF10) B = 1.7280 C = 0.01342
    // wavelength between 400-700nm
    int numWavelengths = 25;
    float step = 0.300 / numWavelengths;
    float wavelength = 0.400;

    vec3 rgb = vec3(0.f, 0.f, 0.f);
    for (int i = 0; i < numWavelengths; i++) {
        float n = 1.5046 + (0.00420 / (wavelength * wavelength));
        float ratio = 1.00 / n;
        
        vec3 I = normalize(Position - cameraPos);
        vec3 R = refract(I, normalize(Normal), ratio);
        vec3 skybox = texture(skybox, R).rgb;
        
        vec3 spectral = texture(spectrum, vec2(i * (1.0 / numWavelengths))).rgb * 0.18;

        rgb += skybox * spectral;

        wavelength += step;
    }
    FragColor = vec4(rgb, 1.0);
}


// 