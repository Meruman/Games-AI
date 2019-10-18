import retro

env = retro.make(game='SonicTheHedgehog-Genesis', state='GreenHillZone.Act1')

env.reset()

done = False

while not done:
    env.render()

    #action = env.action_space.sample()
    action = [0,0,1,0,0,0,0,1,1,1,0,0]
    #print(action)

    ob, rew, done, info = env.step(action)
    #print("Action ", action)
    print("Image", ob.shape, "Reward", rew, "Done?", done)
    #print("Info ", info)