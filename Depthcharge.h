typedef struct vec2
{
    float x;
    float y;

} vec2_t;

typedef struct background
{
    vec2_t position;
    vec2_t size;

} background_t;

typedef struct bullet
{
    unsigned int damage;
    float speed;
    float direction;

} bullet_t;

// to implement
typedef struct bullet_manager
{
    // list of bullets
    unsigned int temp;

} bullet_manager_t;

typedef struct ship
{
    vec2_t position;
    vec2_t size;
    bullet_manager_t *bulletManager;
    float direction;
    float speed;

} ship_t;

enum submarine_type
{
    ten_points = 0,
    twenty_points,
    thirty_points,
    submarine_type_last

};

typedef struct submarine
{
    vec2_t position;
    unsigned int points;
    float direction;
    float speed;
    enum submarine_type type;

} submarine_t;

typedef struct game_logic
{
    unsigned int timer;
} game_logic_t;