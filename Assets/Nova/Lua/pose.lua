local poses = {
    ['ergong'] = {
        ['normal'] = {'body', 'mouth_smile', 'eye_normal', 'eyebrow_normal', 'hair'},
    },
    ['gaotian'] = {
        ['normal'] = {'body', 'mouth_smile', 'eye_normal', 'eyebrow_normal', 'hair'},
        ['cry'] = {'body', 'mouth_smile', 'eye_cry', 'eyebrow_normal', 'hair'},
    },
    ['qianye'] = {
        ['normal'] = {'body', 'mouth_close', 'eye_normal', 'eyebrow_normal', 'hair'},
    },
    ['xiben'] = {
        ['normal'] = {'body', 'mouth_close', 'eye_normal', 'eyebrow_normal', 'hair'},
    },
    ['gg'] = {
        ['normal'] = {'body', 'face-normal'},
        ['sad'] = {'body', 'gg_face_pose1_sad'},
        ['chto'] = {'gg_pose2_hands', 'gg_face_pose2_chto'},
        ['neponial'] = {'gg_pose1_hand', 'gg_face_pose1_neponyal'},
    },
    ['iv'] = {
        ['normal'] = {'body_1', 'face_normal', 'hand_normal'},
        ['laugh'] = {'body_1', 'face_laugh', 'hand_normal'},
        ['angry'] = {'body_1', 'face_angry', 'hand_normal'},
        ['angry'] = {'body_1', 'face_scary', 'hand_normal'},
        ['click1'] = {'body_1', 'face_normal', 'hand_click_1'},
        ['click2'] = {'body_1', 'face_normal', 'hand_click_2'},
        ['shovel'] = {'body_2', 'face_normal', 'hand_normal'},
    },
    ['oks'] = {
        ['normal'] = {'body_1', 'face_normal'},
        ['rest'] = {'body_2', 'face_normal'},
        ['krivlyaka1'] = {'ox_krivlyaka_1'},
        ['krivlyaka2'] = {'ox_krivlyaka_2'},
        ['fear'] = {'ox_p2', 'Ox_p2_face_fear'},
        ['normal2'] = {'ox_p2', 'Ox_p2_face_normal'},
    }
}

function get_pose(obj, pose_name)
    return poses[obj.luaGlobalName] and poses[obj.luaGlobalName][pose_name]
end
