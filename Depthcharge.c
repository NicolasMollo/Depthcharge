#include "Depthcharge.h"
#define SDL_MAIN_HANDLED
#include <SDL.h>
#define STB_IMAGE_IMPLEMENTATION
#include "stb_image.h"

int win_width;
int win_height;

static void init_background(background_t *background)
{
    background->position.x = 0;
    background->position.y = 0;
    background->size.x = 0;
    background->size.y = 0;
}
static void init_ship(ship_t *ship)
{
    // ship->bulletManager = NULL;
    ship->direction = 0;
    ship->speed = 75;
    ship->position.x = 0;
    ship->position.y = 29;
}
static void init_bullet(bullet_t *bullet, const int _damage)
{
    bullet->damage = _damage;
    bullet->direction = 1;
    bullet->speed = 1;
}
static void init_submarine(submarine_t *submarine, enum submarine_type type, const int win_width, const float pos_y)
{
    if (type == ten_points || type == submarine_type_last)
    {
        submarine->type = ten_points;
        submarine->points = 10;
    }
    else if (type == twenty_points)
    {
        submarine->type = twenty_points;
        submarine->points = 20;
    }
    else
    {
        submarine->type = thirty_points;
        submarine->points = 30;
    }

    submarine->speed = submarine->points;
    submarine->direction = 0;
    submarine->position.x = win_width + 10;
    submarine->position.y = pos_y;
}

static void init_game_logic(game_logic_t *game_logic, const int timer)
{
    game_logic->timer = timer;
}

static int init_graphic(SDL_Window **window, SDL_Renderer **renderer, SDL_Texture **background_texture, SDL_Texture **ship_texture) // return 0 if all it's ok.
{
    if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_AUDIO | SDL_INIT_GAMECONTROLLER | SDL_INIT_EVENTS) != 0)
    {
        SDL_Log("Unable to initialize SDL: %s", SDL_GetError());
        return -1;
    }

    const char *win_title = "Depthcharge";
    const vec2_t win_scale = {500, 500};
    win_width = win_scale.x;
    win_height = win_scale.y;
    const vec2_t win_position = {win_scale.x * 0.37, win_scale.y * 0.37};
    *window = SDL_CreateWindow(win_title, win_position.x, win_position.y, win_scale.x, win_scale.y, 0);

    if (!*window)
    {
        SDL_Log("Your window is not correctly created: %s", SDL_GetError());
        goto QUIT;
    }

    *renderer = SDL_CreateRenderer(*window, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);

    if (!*renderer)
    {
        SDL_Log("Your renderer is not correctly created: %s", SDL_GetError());
        SDL_DestroyWindow(*window);
        goto QUIT;
    }

    const char *shiptexture_name = "Graphic/Ship.png";
    int shiptexture_width;
    int shiptexture_height;
    int shiptexture_channels;

    unsigned char *shiptexture_pixels = stbi_load(shiptexture_name, &shiptexture_width, &shiptexture_height, &shiptexture_channels, 4);

    if (!shiptexture_pixels)
    {
        SDL_Log("Unable to open your image: %s", SDL_GetError());
        SDL_DestroyRenderer(*renderer);
        SDL_DestroyWindow(*window);
        goto QUIT;
    }

    SDL_Log("Ship width: %d\nShip height: %d\nShip Channels: %d", shiptexture_width, shiptexture_height, shiptexture_channels);
    *ship_texture = SDL_CreateTexture(*renderer, SDL_PIXELFORMAT_RGBA32, SDL_TEXTUREACCESS_STATIC, shiptexture_width, shiptexture_height);

    if (!*ship_texture)
    {
        SDL_Log("Your texture is not correctly created: %s", SDL_GetError());
        free(shiptexture_pixels);
        SDL_DestroyRenderer(*renderer);
        SDL_DestroyWindow(*window);
        goto QUIT;
    }

    SDL_UpdateTexture(*ship_texture, NULL, shiptexture_pixels, shiptexture_width * 4);
    SDL_SetTextureAlphaMod(*ship_texture, 255);
    SDL_SetTextureBlendMode(*ship_texture, SDL_BLENDMODE_BLEND);
    free(shiptexture_pixels);

    const char *backgroundtexture_name = "Graphic/Background.png";
    int backgroundtexture_width;
    int backgroundtexture_height;
    int backgroundtexture_channels;

    unsigned char *backgroundtexture_pixels = stbi_load(backgroundtexture_name, &backgroundtexture_width, &backgroundtexture_height, &backgroundtexture_channels, 4);

    if (!backgroundtexture_pixels)
    {
        SDL_Log("Unable to open your image: %s", SDL_GetError());
        SDL_DestroyTexture(*ship_texture);
        free(shiptexture_pixels);
        SDL_DestroyRenderer(*renderer);
        SDL_DestroyWindow(*window);
        goto QUIT;
    }

    SDL_Log("Background width: %d\nBackground height: %d\nBackground Channels: %d", backgroundtexture_width, backgroundtexture_height, backgroundtexture_channels);
    *background_texture = SDL_CreateTexture(*renderer, SDL_PIXELFORMAT_RGBA32, SDL_TEXTUREACCESS_STATIC, backgroundtexture_width, backgroundtexture_height);

    if (!*background_texture)
    {
        SDL_Log("Your texture is not correctly created: %s", SDL_GetError());
        free(backgroundtexture_pixels);
        SDL_DestroyTexture(*ship_texture);
        free(shiptexture_pixels);
        SDL_DestroyRenderer(*renderer);
        SDL_DestroyWindow(*window);
        goto QUIT;
    }

    SDL_UpdateTexture(*background_texture, NULL, backgroundtexture_pixels, backgroundtexture_width * 4);
    free(backgroundtexture_pixels);

    return 0;

QUIT:
    SDL_Quit();
    return -1;
}

static void ship_movement(ship_t *ship, double _deltatime)
{
    // CONTRACTED FORM (working)
    // ship.position.x -= ship.position.x <= 0 ? 0 : (keys[SDL_SCANCODE_A] || keys[SDL_SCANCODE_LEFT]) * (ship.speed * deltatime);
    // ship.position.x += ship.position.x >= win_width - 96 ? 0 : (keys[SDL_SCANCODE_D] || keys[SDL_SCANCODE_RIGHT]) * (ship.speed * deltatime);

    int diff_tship_win = win_width - 96;
    const Uint8 *keys = SDL_GetKeyboardState(NULL);

    if (ship->position.x <= 0)
    {
        ship->position.x = 0;
    }
    else if (ship->position.x >= diff_tship_win)
    {
        ship->position.x = diff_tship_win;
    }

    ship->position.x -= (keys[SDL_SCANCODE_A] || keys[SDL_SCANCODE_LEFT]) * (ship->speed * _deltatime);
    ship->position.x += (keys[SDL_SCANCODE_D] || keys[SDL_SCANCODE_RIGHT]) * (ship->speed * _deltatime);
}

static void submarine_movement(submarine_t *submarine, vec2_t *start_pos, double _deltatime)
{
    int offset_pos_x = win_width + 100;
    int pos_y = win_height * 0.5;
    // start_pos->y = _In_range_impl_(0, )
    start_pos->x = offset_pos_x;
    start_pos->y = pos_y;

    submarine->direction = -1;
    submarine->position.x += submarine->direction * (submarine->speed * _deltatime);
}

int main(int argc, char **argv)
{
    background_t background;
    ship_t ship;

    SDL_Renderer *renderer;
    SDL_Window *window;
    SDL_Texture *background_texture;
    SDL_Texture *ship_texture;
    SDL_Texture *submarine_texture;

    init_background(&background);
    init_ship(&ship);
    if (init_graphic(&window, &renderer, &background_texture, &ship_texture) != 0)
    {
        SDL_Log("Something went wrong while tht graphic initializing");
        return -1;
    }

    int running = 1;

    Uint64 current_frame = SDL_GetPerformanceCounter();
    Uint64 previous_frame = 0;
    double deltatime = 0;
    // int is_controllable = 1;

    while (running)
    {
        SDL_Event event;
        while (SDL_PollEvent(&event))
        {
            if (event.type == SDL_QUIT)
            {
                running = 0;
            }
        }

        previous_frame = current_frame;
        current_frame = SDL_GetPerformanceCounter();
        deltatime = (double)(current_frame - previous_frame) / (double)SDL_GetPerformanceFrequency();

        SDL_PumpEvents();

        // input / movements
        ship_movement(&ship, deltatime);

        // renderer
        SDL_SetRenderDrawColor(renderer, 50, 0, 0, 255);
        SDL_RenderClear(renderer);

        // textures
        SDL_Rect target_back_rect = {background.position.x, background.position.y, 500, 500};
        SDL_Rect target_ship_rect = {ship.position.x, ship.position.y, 100, 100};
        // keep ship inside screen
        // keep_ship_inside_screen(&ship);
        SDL_Log("PLAYER POS: %d", target_ship_rect.x);

        SDL_RenderCopy(renderer, background_texture, NULL, &target_back_rect);
        SDL_RenderCopy(renderer, ship_texture, NULL, &target_ship_rect);
        SDL_RenderPresent(renderer);
    }

    return 0;
}