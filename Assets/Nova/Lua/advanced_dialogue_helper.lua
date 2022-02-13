-- jump (lazy only)
cam_size = 5.4
scale_sprite = 0.75
cam_pose_center = {-0.4,0, cam_size}
cam_pose_left = {-9.5,0,cam_size}
cam_pose_right = {9.5, 0, cam_size}

cam_pose_rec_right = {9, 0, cam_size}
cam_pose_rec_left = {-7, 0 , cam_size}

pos_gg = {6.05, -0.55, 0.75, 0, 0}
pos_gg_res_iv = {16.02, -0.55, scale_sprite, 0, 0}
pos_gg_res_oks = {-0.78, -0.55, scale_sprite, 0, 0}
pos_oks = {-8.72, 1.74, scale_sprite, 10.3, 0}
pos_iv = {6.7, 1.4, scale_sprite, 10.3, 0}
pos_pc = {9.7, -0.16}
pos_oks_right = {1.03,nil,nil}
pos_oks_right_show = {1.03, 1.74,scale_sprite, 10.3}
pos_oks_kriv = {1.12, 1.14,scale_sprite, 10.3}
function jmp(to)
    __Nova.advancedDialogueHelper:Jump(to)
end

-- override next dialogue text (lazy only)
function override_text(to)
    __Nova.advancedDialogueHelper:Override(to)
end

-- automatically execute the next dialogue (lazy only)
function fall_through()
    __Nova.advancedDialogueHelper:FallThrough()

function show_prefab(prefab, coord)
    show(__Nova.prefabLoader, prefab, coord)
end

function show_reception()
    anim:trans(cam, function()
        show(bg,"res-5",{0,0,1,10})
        show(bg1,"res-4",{0,0,1,11})
        show(bg2,"res-3",{0,0,1,12})
        show(bg3,"res-2",{0,0,1,13})
        show(bg4,"res-1",{0,0,1,14})
    end, 'fade', 2, { _Mask = 'Masks/eye' })
end

function init_scene()
    anim:move(cam, cam_pose_center)
    
    anim:trans_fade(cam, function()
        show(bg, 'hell-1', {0,0,1,10})
        show(bg1, 'hell-2', {0,0,1,11})
        show(bg2, 'hell-3', {0,0,1,12})
        show(bg3, 'hell-4', {0,0,1,13})
    end, 2)

    anim_persist_begin()
    anim_persist:loop(function(entry)
        return entry:tint(bg3, {0.66, 0.72, 1}, 50):tint(bg3, {0.9, 0.9, 1}, 3):tint(bg3, {1, 0.63, 6}, 3):tint(bg3, {1, 1, 1}, 5)
    end)
    --anim_persist:loop(function(entry)
       -- return entry:tint(bg2, {0.72, 0.78, 1}, 45):tint(bg2, {0.9, 0.9, 1}, 3):tint(bg2, {0.8, 0.8, 1}, 3):tint(bg2, {1, 1, 1}, 5)
    --end)
    --anim_persist:loop(function(entry)
        --return entry:tint(bg1, {0.7, 0.7, 1}, 50):tint(bg1, {0.9, 0.9, 1}, 3):tint(bg1, {0.8, 0.8, 1}, 3):tint(bg1, {1, 1, 1}, 5)
    --end)
    box_hide_show(2)
end

function init_scene()
    anim:trans(cam, function()
        show(bg, 'hell-1', {0,0,1,10})
        show(bg1, 'hell-2', {0,0,1,11})
        show(bg2, 'hell-3', {0,0,1,12})
        show(bg3, 'hell-4', {0,0,1,13})
        show(fg, 'shadow_back')
    end, 'fade', 2, { _Mask = 'Masks/eye' })

    move(cam, cam_pose_center)

    anim_persist_begin()
    anim_persist:loop(function(entry)
        return entry:tint(bg3, {0.66, 0.72, 1}, 50):tint(bg3, {0.9, 0.9, 1}, 3):tint(bg3, {1, 0.63, 6}, 3):tint(bg3, {1, 1, 1}, 5)
    end)
    box_hide_show(2)
end

function show_reception_right()
    hide_bg()
    show_reception() 
    move(cam,cam_pose_rec_right)
    show(gg, 'normal', pos_gg_res_iv) 
    show(iv,'normal', pos_iv) 
    show(oks,'normal', pos_oks)
end

function show_reception_left()
    hide_bg()
    show_reception() 
    move(cam,cam_pose_rec_left)
    show(gg, 'normal', pos_gg_res_oks) 
    show(iv,'normal', pos_iv) 
    show(oks,'normal', pos_oks)
end

function hide_bg()
    hide(oks)
    hide(iv)
    hide(gg)
    show(fg, 'black')
    hide(bg)
    hide(bg1)
    hide(bg2)
    hide(bg3)
    hide(bg4)
    hide(fg)
    hide(fg1)
    hide(fg2)
end

function gotoEnd()
    hide_bg()
    anim:wait(1):action(function()
        init_scene()
        show(gg,'normal', pos_gg)
    end)
end

function flip(obj)
    move(obj, {nil, nil, nil, nil, {0, 180, 0}})
end

function unflip(obj)
    move(obj, {nil, nil, nil, nil, {0, 0, 0}})
end

function play_music(name)
    anim:fade_in(bgm, name, 0.5, 3)
end
